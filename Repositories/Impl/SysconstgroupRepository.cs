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
*│　描    述：{Comment}接口实现                                                    
*│　作    者：https://github.com/KenneyChen                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-12-23 01:44:51                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： NSql2Table.Repository.Impl                                  
*│　类    名： SysconstgroupRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using NSql2Table.Models;
using NSql2Table.Repository;
using System;
using YDal.Repository.Impl;
using YDal.UnitOfWork;

namespace NSql2Table.Repository.Impl
{
    public class SysconstgroupRepository: BaseEfRepository<Sysconstgroup>,ISysconstgroupRepository
    {
        /// <summary>
        ///  获取 仓储上下文的实例
        /// </summary>
        private IUnitOfWork _unitOfWork;

        public SysconstgroupRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
    }
}