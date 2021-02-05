
import Vue from 'vue'
import VueRouter from 'vue-router'
import Layout from '@/layouts'
import EmptyLayout from '@/layouts/EmptyLayout'
import { publicPath, routerMode } from '@/config/settings'

Vue.use(VueRouter)
export const constantRoutes = [
  {
    path: '/login',
    component: () => import('@/views/login/index'),
    hidden: true,
  },
  {
    path: '/register',
    component: () => import('@/views/register/index'),
    hidden: true,
  },
  {
    path: '/401',
    name: '401',
    component: () => import('@/views/401'),
    hidden: true,
  },
  {
    path: '/404',
    name: '404',
    component: () => import('@/views/404'),
    hidden: true,
  },
  {
    path: '/conf/formList',
    name: 'formList',
    component: () => import('@/views/report/formList'),
    hidden: true,
  },
]

export const asyncRoutes = [
  {
    path: '/',
    component: Layout,
    redirect: 'index',
    children: [
      {
        path: 'index',
        name: 'Index',
        component: () => import('@/views/index/index'),
        meta: {
          title: '首页',
          icon: 'home',
          affix: true,
        },
      },
    ],
  },
  {
    path: '/system',
    component: Layout,
    redirect: 'noRedirect',
    name: 'Vab',
    alwaysShow: true,
    meta: { title: '系统配置', icon: 'box-open' },
    children: [
      {
        path: 'ds',
        name: 'dataSource',
        component: () =>
          import('@/views/datasource/index'),
        meta: {
          title: '数据源',
          permissions: ['admin', 'editor']
        }
      },
      {
        path: 'data',
        name: 'dicData',
        component: () =>
          import('@/views/personnelManagement/userManagement/index'),
        meta: {
          title: '字典数据',
          permissions: ['admin', 'editor']
        }
      }
    ]
  },

  {
    path: '/conf',
    component: Layout,
    redirect: 'noRedirect',
    name: 'Vab',
    alwaysShow: true,
    meta: { title: '在线开发', icon: 'box-open' },
    children: [
      {
        path: 'reportconf',
        name: 'reportconf',
        component: () =>
          import('@/views/report/index'),
        meta: {
          title: '报表配置'
        }
      },
     
      {
        path: 'table',
        component: () =>
          import('@/views/personnelManagement/userManagement/index'),
        name: 'Table',
        meta: {
          title: '表单设计',
          permissions: ['admin']
        }
      }
    ]
  },



  {
    path: '*',
    redirect: '/404',
    hidden: true,
  },
]

const router = new VueRouter({
  base: publicPath,
  mode: routerMode,
  scrollBehavior: () => ({
    y: 0,
  }),
  routes: constantRoutes,
})
//注释的地方是允许路由重复点击，如果你觉得框架路由跳转规范太过严格可选择放开
const originalPush = VueRouter.prototype.push
VueRouter.prototype.push = function push(location, onResolve, onReject) {
  if (onResolve || onReject)
    return originalPush.call(this, location, onResolve, onReject)
  return originalPush.call(this, location).catch((err) => err)
}

export function resetRouter() {
  router.matcher = new VueRouter({
    base: publicPath,
    mode: routerMode,
    scrollBehavior: () => ({
      y: 0,
    }),
    routes: constantRoutes,
  }).matcher
}

export default router
