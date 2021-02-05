<template>
  <el-dialog :title="title"
             :visible.sync="dialogFormVisible"
             width="1200px"
             @close="close">
    <el-form ref="form" :model="form" :rules="rules" :inline="true" class="demo-form-inline">
      <el-card class="box-card">
        <div class="text item">
          <el-row>
            <el-col :span="14">
              <el-form-item label="模板名称" prop="TemplateName">
                <el-input v-model="form.templateName" placeholder="模板名称"></el-input>
              </el-form-item>
              <el-form-item label="数据源" prop="DsSourceId">
                <el-select v-model="form.dsSourceId" placeholder="数据源">
                  <el-option v-for="item in dsData" :key="item.id" :label="item.name" :value="item.id"></el-option>
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="10" style="text-align:left">

            </el-col>
          </el-row>


          <el-row>
            <el-col :span="14">
              <el-form-item label="SQL脚本" title="参数化请使用@符号，比如name=@name" prop="SqlString">
                <el-input v-model="form.sqlString" type="textarea" :cols="70" :rows="10"></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="10" style="text-align:left">
              <el-button type="success" @click="sqlResolve()">SQL解析</el-button>
            </el-col>
          </el-row>


        </div>
      </el-card>

      <el-row>
        <el-col :span="24">
          <el-card class="box-card">
            <div slot="header" class="clearfix">
              <span>参数列表</span>
              <el-button type="primary" style="float: right;" icon="el-icon-plus" @click="queryAddRow">新增一行</el-button>
            </div>
            <div class="text item">
              <el-table :data="queryTableData"
                        style="width: 100%">
                <el-table-column prop="tableColumnName"
                                 label="显示名称"
                                 width="180">
                  <template slot-scope="scope">
                    <el-input v-model="scope.row['tableColumnName']" />
                  </template>
                </el-table-column>
                <el-table-column prop="dbColumnName"
                                 label="数据库字段"
                                 width="180">
                  <template slot-scope="scope">
                    <el-input v-model="scope.row['dbColumnName']" />
                  </template>
                </el-table-column>
                <el-table-column prop="tableColumnType"
                                 label="字段类型">
                  <template slot-scope="scope">
                    <el-select v-model="scope.row['tableColumnType']" placeholder="字段类型">
                      <el-option v-for="item in tableColumTypes"
                                 :key="item.id"
                                 :label="item.value"
                                 :value="item.id">
                      </el-option>
                    </el-select>
                  </template>
                </el-table-column>
                <el-table-column prop="tableColumnDefaultValue"
                                 label="默认值">
                  <template slot-scope="scope">
                    <el-input v-model="scope.row['tableColumnDefaultValue']" />
                  </template>
                </el-table-column>
                <el-table-column prop="tableDefaultValueCode"
                                 label="字典code">
                  <template slot-scope="scope">
                    <el-input v-model="scope.row['tableDefaultValueCode']" />
                  </template>
                </el-table-column>
                <el-table-column prop="isShow"
                                 label="是否显示">
                  <template slot-scope="scope">
                    <!-- `checked` 为 true 或 false -->
                    <el-checkbox v-model="scope.row['isShow']"></el-checkbox>
                  </template>
                </el-table-column>
                <el-table-column prop="queryMode"
                                 label="查询模式">
                  <template slot-scope="scope">
                    <el-select v-model="scope.row['queryMode']" placeholder="查询模式">
                      <el-option v-for="item in queryModes"
                                 :key="item.id"
                                 :label="item.value"
                                 :value="item.id">
                      </el-option>
                    </el-select>
                  </template>
                </el-table-column>
              </el-table>
            </div>
          </el-card>
        </el-col>
      </el-row>


      <el-row>
        <el-col :span="24">
          <el-card class="box-card">
            <div slot="header" class="clearfix">
              <span>返回列表</span>
              <el-button type="primary" style="float: right;" icon="el-icon-plus" @click="resultAddRow">新增一行</el-button>
            </div>
            <div class="text item">
              <el-table :data="resultTableData"
                        style="width: 100%">
                <el-table-column prop="tableColumnName"
                                 label="显示名称"
                                 width="180">
                  <template slot-scope="scope">
                    <el-input v-model="scope.row['tableColumnName']" />
                  </template>
                </el-table-column>
                <el-table-column prop="dbColumnName"
                                 label="数据库字段"
                                 width="180">
                  <template slot-scope="scope">
                    <el-input v-model="scope.row['dbColumnName']" />
                  </template>
                </el-table-column>
                <el-table-column prop="tableColumnType"
                                 label="字段类型"
                                 width="180">
                  <template slot-scope="scope">
                    <el-select v-model="scope.row['tableColumnType']" placeholder="字段类型">
                      <el-option v-for="item in tableColumTypes"
                                 :key="item.id"
                                 :label="item.value"
                                 :value="item.id">
                      </el-option>
                    </el-select>
                  </template>
                </el-table-column>
                <el-table-column prop="isShow"
                                 label="是否显示"
                                 width="180">
                  <template slot-scope="scope">
                    <!-- `checked` 为 true 或 false -->
                    <el-checkbox v-model="scope.row['isShow']"></el-checkbox>
                  </template>
                </el-table-column>
              </el-table>
            </div>
          </el-card>
        </el-col>
      </el-row>
    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button type="primary" @click="save">确认</el-button>
      <el-button @click="close">关闭</el-button>
    </div>
  </el-dialog>
</template>

<script>
  import { doEdit } from '@/api/reportconf'
  import { getList } from '@/api/dataSource'
  export default {
    name: 'ReportEdit',
    data() {
      return {
        dsData: [],
        queryForm: {
          pageNo: 1,
          pageSize: 1000,
          username: '',
        },
        tableColumTypes: [{
          id: 1,
          value: '文本框'
        },
        {
          id: 2,
          value: '日期(yyyy-MM-dd)'
        },
        {
          id: 3,
          value: '下拉框'
        }],
        queryModes: [{
          id: 1,
          value: '单条件查询'
        },
        {
          id: 2,
          value: '范围查询'
        }],

        queryTableData: [{
          tableColumnName: '订单号id',
          dbColumnName: 'orderId',
          tableColumnType: 1,
          tableColumnDefaultValue: '',
          tableDefaultValueCode: '',
          isShow: true,
          queryMode: 1
        }, {
          tableColumnName: '订单号id',
          dbColumnName: 'orderId',
          tableColumnType: 1,
          tableColumnDefaultValue: '',
          tableDefaultValueCode: '',
            isShow: true,
          queryMode: 1
        },
        {
          tableColumnName: '订单号id',
          dbColumnName: 'orderId',
          tableColumnType: 1,
          tableColumnDefaultValue: '',
          tableDefaultValueCode: '',
          isShow: true,
          queryMode: 1
        }],

        resultTableData: [{
          tableColumnName: '订单号id',
          dbColumnName: 'orderId',
          tableColumnType: 1,
          isShow: true
        }, {
          tableColumnName: '订单号id',
          dbColumnName: 'orderId',
          tableColumnType: 1,
          isShow: true
        },
        {
          tableColumnName: '订单号id',
          dbColumnName: 'orderId',
          tableColumnType: 1,
          isShow: true
        }],
        form: {
          sqlString: 'select id from user where id=@id and name=@name',
          dsSourceId: '',
          templateName: ''
        },
        rules: {
         sqlString: [
            { required: true, trigger: 'blur', message: '请输入sql语句' }
          ],
          dsSourceId: [
            { required: true, trigger: 'blur', message: '请选择数据源' }
          ],
          templateName: [
            { required: true, trigger: 'blur', message: '模板名称' }
          ],
        },
        title: '',
        dialogFormVisible: false
      }
    },
    created() {
      this.loadDsData();
    },
    methods: {
      sqlResolve() {
        debugger;
        //匹配select字段
        var regSelect = /select\s+.*?\s+from\s+/i;
        var sql = this.form.sqlString;
        var arr = regSelect.exec(sql);
        if (arr != null && arr.length > 0) {

          var r1 = arr[0].trim()
          var cols = r1.replace(/^Select/ig, '').replace(/From$/ig, '').split(',');

          if (cols != null && cols.length > 0) {
            //清空原来数组
            this.resultTableData = [];
            cols.forEach(f => {
              var col = {
                tableColumnName: f,
                dbColumnName: f.split('.').length > 1 ? f.split('.')[1]:f,
                tableColumnType: 1,
                tableColumnDefaultValue: '',
                tableDefaultValueCode: '',
                isShow: true,
                queryMode: 1
              };
              this.resultTableData.push(col);
            })
          }
        } else {
          this.$baseMessage('select语句可能存在问题，程序找不到匹配列名', 'success')
        }

        //匹配where条件
        var regConditionConf = /(@\w+)+/ig;
        var resultCols = sql.match(regConditionConf);
        if (resultCols != null && resultCols.length > 0) {
          //清空原来数组
          this.queryTableData = [];
          resultCols.forEach(f => {
            var filterName = f.replace('@', '');
            var col = {
              tableColumnName: filterName,
              dbColumnName: filterName,
              tableColumnType: 1,
              isShow: true
            };
            this.queryTableData.push(col);
          })
        }
        debugger;
      },
      queryAddRow() {
        let $query = this.queryTableData;
        if ($query.length > 0) {
          var len = $query.length - 1;
          var last = $query[len];
          $query.push(last);
        } else {
          $query.push({
            tableColumnName: '订单号id',
            dbColumnName: 'orderId',
            tableColumnType: 1,
            tableColumnDefaultValue: '',
            tableDefaultValueCode: '',
            isShow: true,
            queryMode: 1
          });
        }
      },
      resultAddRow() {
        let $result = this.resultTableData;
        if ($result.length > 0) {
          var len = $result.length - 1;
          var last = $result[len];
          $result.push(last);
        } else {
          $result.push({
            tableColumnName: '订单号id',
            dbColumnName: 'orderId',
            tableColumnType: 1,
            tableColumnDefaultValue: '',
            tableDefaultValueCode: '',
            isShow: true,
            queryMode: 1
          });
        }
      },
      async loadDsData() {
        debugger;
        let queryDs = {
          pageNo: 1,
          pageSize: 1000,
          username: '',
        };
        var rsp = await getList(queryDs);
        debugger;
        const { records, totalCount } = rsp.data;
        this.dsData = records;
      },
      showEdit(row) {
        if (!row) {
          this.title = '添加模板配置'
        } else {
          this.title = '编辑模板配置'
          this.form = Object.assign({}, row)
        }
        this.dialogFormVisible = true
      },
      close() {
        //this.$refs['form'].resetFields()
        this.form = this.$options.data().form
        this.dialogFormVisible = false
      },
      save() {
        debugger;
        this.form.resultList = this.resultTableData;
        this.form.conditionList = this.queryTableData;
        this.$refs['form'].validate(async (valid) => {
          debugger;
          if (valid) {
            debugger;
            const { msg } = await doEdit(this.form)
            this.$baseMessage(msg, 'success')
            this.$emit('fetch-data')
            this.close()
          } else {
            return false
          }
        })
      },
    },
  }
</script>
