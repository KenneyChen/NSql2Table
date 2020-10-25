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
*│　描    述：                                                    
*│　作    者：https://github.com/KenneyChen                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-10-27 02:26:30                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: NSql2Table.Models                                  
*│　类    名：Sysconst                                     
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YDal.Models;

namespace NSql2Table.Models
{
    /// <summary>
    /// https://github.com/KenneyChen
    /// 2020-10-27 02:26:30
    /// 
    /// </summary>
    [Table("sysconst")]
    public partial class Sysconst:BaseEntity
    {
        	[Key]
		[Column("Id")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Int32 Id {get;set;}


		[Column("ConstKey")]
		[Required]
		[MaxLength(50)]
		public String ConstKey {get;set;}


		[Column("ConstValue")]
		[Required]
		[MaxLength(50)]
		public String ConstValue {get;set;}


		[Column("Status")]
		[Required]
		public SByte Status {get;set;}


		[Column("OrderBy")]
		[Required]
		public Int32 OrderBy {get;set;}


		[Column("GroupId")]
		[Required]
		public Int32 GroupId {get;set;}



    }
}