import request from '@/utils/request'

export function getList(data) {
  return request({
    url: '/ds/getList',
    method: 'get',
    data,
  })
}

export function doEdit(data) {
  return request({
    url: '/ds/save',
    method: 'post',
    contentType:'application/json',
    data,
  })
}

export function doDelete(data) {
  return request({
    url: '/userManagement/doDelete',
    method: 'post',
    data,
  })
}
