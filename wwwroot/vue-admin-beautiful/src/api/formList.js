import request from '@/utils/request'

export function fetch(id) {
  return request({
    url: '/SqlTemlate/fetch/'+id,
    method: 'get'
  })
}

export function search(data) {
  return request({
    url: '/SqlTemlate/search',
    method: 'post',
    //headers: data.getHeaders(),
    contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
    data,
  })
}

