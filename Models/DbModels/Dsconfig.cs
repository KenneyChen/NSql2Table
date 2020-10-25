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
*│　类    名：Dsconfig                                     
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
    [Table("dsconfig")]
    public partial class Dsconfig : BaseEntity
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }


        [Column("Code")]
        [Required]
        [MaxLength(50)]
        public String Code { get; set; }


        [Column("Name")]
        [Required]
        [MaxLength(200)]
        public String Name { get; set; }


        [Column("DbType")]
        [Required]
        public Int32 DbType { get; set; }


        [Column("DbConnection")]
        [Required]
        [MaxLength(2500)]
        public String DbConnection { get; set; }


        [Column("CreatedById")]
        public Int32? CreatedById { get; set; }


        [Column("CreatedByName")]
        [MaxLength(50)]
        public String CreatedByName { get; set; }


        [Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }


        [Column("ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }


        [Column("ModifiedByName")]
        [MaxLength(50)]
        public String ModifiedByName { get; set; }



    }
}