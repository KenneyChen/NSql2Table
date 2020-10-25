/*
                           _ooOoo_
                          o8888888o
                          88" . "88
                          (| -_- |)
                          O\  =  /O
                       ____/`---'\____
                     .'  \\|     |//  `.
                    /  \\|||  :  |||//  \
                   /  _||||| -:- |||||-  \
                   |   | \\\  -  /// |   |
                   | \_|  ''\---/''  |   |
                   \  .-\__  `-`  ___/-. /
                 ___`. .'  /--.--\  `. . __
              ."" '<  `.___\_<|>_/___.'  >'"".
             | | :  `- \`.;`\ _ /`;.`/ - ` : | |
             \  \ `-.   \_ __\ /__ _/   .-` /  /
        ======`-.____`-.___\_____/___.-`____.-'======
                           `=---='
        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                 佛祖保佑       永无BUG
*/
/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：{Comment}                                                    
*│　作    者：{Author}                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：{GeneratorTime}                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： NSql2Table.Services.Impl                                  
*│　类    名： SqlfunctionsService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using NetCore.Dal.Services.Impl;
using NSql2Table.Models;
using NSql2Table.Repository;
using NSql2Table.Services;
using System;
using System.Linq;
using NetCore.Dal.Services;

namespace NSql2Table.Services.Impl
{
    public class SqlfunctionsService: BaseService<ISqlfunctionsRepository, Sqlfunctions>,
        ISqlfunctionsService
    {
        private readonly ISqlfunctionsRepository _repository;

        public SqlfunctionsService(ISqlfunctionsRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}