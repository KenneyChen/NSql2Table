<template>
  <el-dialog :title="title"
             :visible.sync="dialogFormVisible"
             width="600px"
             @close="close">
    <el-form ref="form" :model="form" :rules="rules" label-width="120px">
      <el-form-item label="项目名字" prop="name">
        <el-input v-model.trim="form.name" autocomplete="off"></el-input>
      </el-form-item>

      <el-form-item label="类型" prop="email">
        <el-select v-model.trim="form.dbType" placeholder="类型">
          <el-option v-for="item in dbTypeOptions"
                     :key="item.value"
                     :label="item.label"
                     :value="item.value">
          </el-option>
        </el-select>
      </el-form-item>

      <el-form-item label="连接字符串" prop="dbConnection">
        <el-input v-model="form.dbConnection"
                  type="textarea"
                  :rows="5"
                  placeholder="请输入内容"
                 >
        </el-input>
      </el-form-item>

    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </div>
  </el-dialog>
</template>

<script>
  import { doEdit } from '@/api/dataSource'

  export default {
    name: 'DsEdit',
    data() {
      return {
        form: {
          name: '',
          dbConnection: '',
          dbType: 0,
        },
        rules: {
          name: [
            { required: true, trigger: 'blur', message: '项目名' },
          ],
          dbConnection: [
            { required: true, trigger: 'blur', message: '请输入数据库连接字符串' },
          ],
          dbType: [{ required: true, trigger: 'blur', message: '请选择数据库类型' }],
        },
        title: '',
        dialogFormVisible: false,

        dbTypeOptions: [{
          value: 0,
          label: 'MySql'
        }, {
          value: 1,
          label: 'MSSQL'
        }],

      }
    },
    created() { },
    methods: {
      showEdit(row) {
        if (!row) {
          this.title = '添加数据源'
        } else {
          this.title = '编辑数据源'
          this.form = Object.assign({}, row)
        }
        this.dialogFormVisible = true
      },
      close() {
        this.$refs['form'].resetFields()
        this.form = this.$options.data().form
        this.dialogFormVisible = false
      },
      save() {
        this.$refs['form'].validate(async (valid) => {
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
