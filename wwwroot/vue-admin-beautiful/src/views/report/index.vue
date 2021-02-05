<template>
  <div class="userManagement-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="12">
        <el-form :inline="true" :model="queryForm" @submit.native.prevent>
          <el-form-item label="模板编码:">

            <el-input v-model.trim="queryForm.username"
                      placeholder="模板编码"
                      clearable />

          </el-form-item>

          <el-form-item label="模板名字：">

            <el-input v-model.trim="queryForm.username"
                      placeholder="模板名字"
                      clearable />
          </el-form-item>
          <el-form-item>
            <el-button icon="el-icon-search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
          <el-form-item>
            <el-button icon="el-icon-plus" type="primary" @click="handleEdit">
              添加
            </el-button>
          </el-form-item>
          <el-form-item>
            <el-button icon="el-icon-delete" type="danger" @click="handleDelete">
              批量删除
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-left-panel>
    </vab-query-form>

    <el-table v-loading="listLoading"
              :data="list"
              :element-loading-text="elementLoadingText"
              @selection-change="setSelectRows">
      <el-table-column show-overflow-tooltip type="selection"></el-table-column>
      <el-table-column show-overflow-tooltip
                       prop="sqltemlateCode"
                       label="模板编码"></el-table-column>

      <el-table-column show-overflow-tooltip
                       prop="templateName"
                       label="模板名称"></el-table-column>

      <el-table-column show-overflow-tooltip
                       prop="sqlString"
                       label="SQL"></el-table-column>


      <el-table-column show-overflow-tooltip
                       prop="modifiedDate"
                       label="修改时间"></el-table-column>
      <el-table-column show-overflow-tooltip label="操作" width="200">
        <template #default="{ row }">
          <el-button type="text" @click="handleEdit(row)">编辑</el-button>
          <el-button type="text" @click="handleDelete(row)">配置地址</el-button>
          <!--<el-link type="primary" href="/">功能测试</el-link>
          <el-button type="text" @click="handleTest(row)">功能测试</el-button>-->
          <router-link to='/conf/formList'><el-button type="text">功能测试</el-button></router-link>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination background
                   :current-page="queryForm.pageNo"
                   :page-size="queryForm.pageSize"
                   :layout="layout"
                   :total="total"
                   @size-change="handleSizeChange"
                   @current-change="handleCurrentChange"></el-pagination>
    <edit ref="edit" @fetch-data="fetchData"></edit>
  </div>
</template>

<script>
  import { getList, doDelete } from '@/api/reportconf'
  import Edit from './components/reportEdit'
  import axios from 'axios'

  export default {
    name: 'Report',
    components: { Edit },
    data() {
      return {
       
        list: null,
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        elementLoadingText: '正在加载...',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          username: '',
        },
      }
    },
    created() {
      this.fetchData()
    },
    methods: {

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
      handleTest(row) {
        if (row.id) {

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
      queryData() {
        this.queryForm.pageNo = 1
        this.fetchData()
      },
      async fetchData() {

        this.listLoading = true
        var rsp = await getList(this.queryForm);
        debugger;
        const { records, totalCount } = rsp.data
        this.list = records
        this.total = totalCount
        setTimeout(() => {
          this.listLoading = false
        }, 300)
      },
    },
  }
</script>
