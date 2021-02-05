import request from '@/utils/request'

export function getList(data) {
  return request({
    url: '/SqlTemlate/getList',
    method: 'get',
    data,
  })
}

export function doEdit(data) {
  return request({
    url: '/SqlTemlate/save',
    method: 'post',
    contentType: 'application/json',
    data,
  })
}

export function doDelete(data) {
  return request({
    url: '/SqlTemlate/doDelete',
    method: 'post',
    data,
  })
}
