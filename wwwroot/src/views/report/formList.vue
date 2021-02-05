<template>
  <div class="userManagement-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="12">
        <el-form :inline="true" @submit.native.prevent>

          <el-form-item v-for="(item,dx) in searchConditions" :key="dx" :label="item.tableColumnName">
            <span v-if="item.tableColumnType==1">
              <!--文本框-->
              <el-input v-model.trim="item.tableCloumnDefaultValue"
                        :placeholder="item.tableColumnName"
                        clearable />
            </span>
            <span v-if="item.tableColumnType==2">
              <!--日期-->
              <el-date-picker v-model="item.tableCloumnDefaultValue"
                              type="date"
                              placeholder="选择日期">
              </el-date-picker>
            </span>
          </el-form-item>
          <el-form-item>
            <el-button icon="el-icon-search" type="primary" @click="handleSearch">
              查询
            </el-button>
          </el-form-item>
          <el-form-item>
            <el-button icon="el-icon-plus" type="primary" @click="handleEdit">
              导出
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-left-panel>
    </vab-query-form>

    <el-table v-loading="listLoading"
              :data="searchResult"
              :element-loading-text="elementLoadingText"
              @selection-change="setSelectRows">
      <!-- <el-table-column show-overflow-tooltip type="selection"></el-table-column>-->
      <el-table-column v-for="(item,dx) in tableHeader"
                       :key="dx"
                       :prop="item.dbColumnName"
                       :label="item.tableColumnName"
                       show-overflow-tooltip></el-table-column>

      <!-- <el-table-column show-overflow-tooltip
                        prop="templateName"
                        label="模板名称"></el-table-column>

       <el-table-column show-overflow-tooltip
                        prop="sqlString"
                        label="SQL"></el-table-column>


       <el-table-column show-overflow-tooltip
                        prop="modifiedDate"
                        label="修改时间"></el-table-column>-->
      <!--  <el-table-column show-overflow-tooltip label="操作" width="200">
          <template #default="{ row }">
            <el-button type="text" @click="handleEdit(row)">编辑</el-button>
            <el-button type="text" @click="handleDelete(row)">配置地址</el-button>
            <el-button type="text" @click="handleDelete(row)">功能测试</el-button>
          </template>
        </el-table-column>-->
    </el-table>
    <edit ref="edit" @fetch-data="fetchData"></edit>
  </div>
</template>

<script>
  import { fetch, search } from '@/api/formList'
  import Edit from './components/reportEdit'
  import axios from 'axios'

  export default {
    name: 'Report',
    components: { Edit },
    data() {
      return {
        searchConditions: [{
          dbColumnName: '姓名',
          tableColumnName: '姓名',
          tableColumnType: 1,
          tableCloumnDefaultValue: '',
          queryMode: 1,//1单条件查询，2范围查询
        }, {
          dbColumnName: '出身年日',
          tableColumnName: '姓名',
          tableColumnType: 2, //1-文本；2-日期；3下拉框；
          tableCloumnDefaultValue: '',
          queryMode: 1,//1单条件查询，2范围查询
        }],

        //搜索结果表格头部列名
        tableHeader: [{
          dbColumnName: 'userName',
          tableColumnName: '姓名'
        }, {
          dbColumnName: 'birthDay',
          tableColumnName: '出生日期'
        }],

        //搜索结果表格数据
        searchResult: null,

        list: null,
        listLoading: false,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        elementLoadingText: '正在加载...',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          username: '',
          id: 15,
        },
      }
    },
    async created() {
      await this.fetchData();
    },
    mounted() {
    },
    methods: {

      test() {
        debugger;
      },
      setSelectRows(val) {
        this.selectRows = val
      },
      handleEdit(row) {
        if (row.id) {
          this.$refs['edit'].showEdit(row)
        } else {
          this.$refs['edit'].showEdit()
        }
      },
      handleDelete(row) {
        if (row.id) {
          this.$baseConfirm('你确定要删除当前项吗', null, async () => {
            const { msg } = await doDelete({ ids: row.id })
            this.$baseMessage(msg, 'success')
            this.fetchData()
          })
        } else {
          if (this.selectRows.length > 0) {
            const ids = this.selectRows.map((item) => item.id).join()
            this.$baseConfirm('你确定要删除选中项吗', null, async () => {
              const { msg } = await doDelete({ ids })
              this.$baseMessage(msg, 'success')
              this.fetchData()
            })
          } else {
            this.$baseMessage('未选中任何行', 'error')
            return false
          }
        }
      },
      handleSizeChange(val) {
        this.queryForm.pageSize = val
        this.fetchData()
      },
      handleCurrentChange(val) {
        this.queryForm.pageNo = val
        this.fetchData()
      },
      postSearch() {
        //根据搜索条件加载默认数据
        let fm = new FormData();
        //数据库唯一键，避免重复，名字定义奇怪点
        fm.append('__search__id', this.queryForm.id);
        this.searchConditions.forEach(f => {
          //数据库字段，对应value
          fm.append(f.dbColumnName, f.tableColumnDefaultValue);
        });
        var jsonData = {};
        fm.forEach((value, key) => jsonData[key] = value);
        search(jsonData).then((rspSearch) => {
          const { records, totalCount } = rspSearch.data;
          this.list = records;
          this.total = totalCount;
        });
      },
      handleSearch() {
        debugger;
        this.queryForm.pageNo = 1;
        this.postSearch();
      },
      async fetchData() {

        this.searchResult = [{
          userName: '张三',
          birthDay: '2020-11-29'
        }, {
          userName: '李四',
          birthDay: '2020-12-30'
        }];
        this.listLoading = true;
        var rsp = await fetch(this.queryForm.id);
        let data = rsp.data;
        //初始化搜索条件vue
        this.searchConditions = data.conditionList;
        //初始化表格vue
        this.tableHeader = data.resultList;

        //开始搜索数据
        //await search();

        setTimeout(() => {
          this.listLoading = false
        }, 300)
      },
    },
  }
  //form-create自定义表单组件：
</script>
