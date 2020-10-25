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
*│　描    述：Sqltemlate                                                    
*│　作    者：https://github.com/KenneyChen                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-11-16 02:36:55                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: NSql2Table.Models                                  
*│　类    名：Sqltemlate                                     
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
    /// 2020-11-16 02:36:55
    /// Sqltemlate
    /// </summary>
    [Table("sqltemlate")]
    public partial class Sqltemlate : BaseEntity
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }


        [Column("DsSourceId")]
        [Required]
        public Int32 DsSourceId { get; set; }


        [Column("SqltemlateCode")]
        [Required]
        public string SqltemlateCode { get; set; }


        [Column("TemplateName")]
        [Required]
        [MaxLength(100)]
        public String TemplateName { get; set; }


        [Column("SqlString")]
        [Required]
        [MaxLength(65535)]
        public String SqlString { get; set; }

        [Column("ModifiedDate")]
        public string ModifiedDate { get; set; }
    }
}