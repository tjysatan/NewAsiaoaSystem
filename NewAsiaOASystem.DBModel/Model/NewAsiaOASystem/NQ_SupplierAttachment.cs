using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    //public enum attachType
    //{
    //    Licence = 0,    // 营业执照
    //    TaxNum = 1,     //税号
    //    ISO9001 = 2,    //iso9001
    //    ISO14000 = 3,   //iso14000
    //    Patent = 4,     //专利证书
    //    Other = 5,      //其他证书
    //    Questionnaire = 6,      //供应商调查表
    //    Agent = 7,      //代理证
    //    SampleEvaluation = 8,   //样品评估
    //    TrialProduction = 9,    //小批量试产
    //    QualityAgreement = 10,  //质量协议
    //    SupplierQuestionnaire = 11 //供应商评估表
    //}
    public class NQ_SupplierAttachment
    {
        public virtual int id { get; set; }

        public virtual int FSupplierid { get; set; }

        //0:营业执照(三证合一), 1:税务登记证, 2:ISO9001, 3:ISO14000,4:专利证书,
        //5:其他认证,6:供应商调查表,7:代理证, 8:样品评估, 9:小批量试产, 10:质量协议
        //11:供应商调查表 
        public virtual int FAttType { get; set; }

        public virtual string FAttURL { get; set; }

        public virtual DateTime FAttDeadline { get; set; }

        public virtual string FAttText { get; set;}

        public virtual int isPermanent { get; set; }

        // 附件序号, 单个类型的附件上传多份
        public virtual int seq { get; set; }



    }
}
