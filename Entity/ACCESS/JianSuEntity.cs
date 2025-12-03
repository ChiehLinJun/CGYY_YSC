namespace CGYY_YSC.Entity.ACCESS
{
    class JianSuEntity : BaseEntity
    {
        public string 标本号 { get; set; }
        public string 样本编号 { get; set; }
        //public string 标本种类 { get; set; }
        public string 送检时间 { get; set; }
        public string 病历号 { get; set; }
        //public string 送检科室 { get; set; }
        //public string 送检医师 { get; set; }
        //public string 检验医师 { get; set; }
        //public string 审核者 { get; set; }
        public string 过氧化氢 { get; set; }       //72-77F
        public string 唾液酸苷酶 { get; set; }   //72-77D
        public string 白细胞酯酶 { get; set; }   //72-77E
        public string Β葡萄糖醛酸酶 { get; set; } //72B77B
        public string 乙酰氨基葡萄糖苷酶 { get; set; }  //72A77B
        public string PH值 { get; set; }     //72-77G
        //public string 乳酸分级 { get; set; }
        public string 菌群密集度 { get; set; }  //72A77C
        public string 菌群多样性 { get; set; }   //72B77C
        public string 优势菌 { get; set; }     //72C77C
        public string 滴虫感染 { get; set; }        //72B77A
        public string 菌丝 { get; set; }      //72C77A
        public string 孢子 { get; set; }      //72D77A
        public string 芽生孢子 { get; set; }    //72E77A
        public string Nugent评分 { get; set; }    //72A77H
        //public string 乳酸杆菌评分 { get; set; }
        public string 白细胞 { get; set; }     //72A77A
        //public string 霉菌孢子 { get; set; }
        //public string 基底上皮细胞 { get; set; }
        //public string 红细胞 { get; set; }
        //public string 霉菌菌丝 { get; set; }
        //public string 上皮细胞满视野 { get; set; }
        //public string 脓细胞 { get; set; }
        //public string 支原体样小体 { get; set; }
        //public string 白脓细胞数量 { get; set; }
        //public string 乳酸杆菌 { get; set; }
        //public string 革兰氏阴性双球菌 { get; set; }
        //public string 衣原体样小体 { get; set; }
        //public string 线索细胞 { get; set; }
        //public string 上皮细胞溶解现象 { get; set; }
        //public string 球菌 { get; set; }
        //public string 球杆菌 { get; set; }
        //public string 双球菌 { get; set; }
        //public string 杆毛菌 { get; set; }
        //public string 诊断结果 { get; set; }
        //public string 上皮细胞 { get; set; }
        //public string 滴虫 { get; set; }
        public string 乳杆分级 { get; set; }//72C77H
        public string AV评分 { get; set; }
        public string 微生态分析 { get; set; }

    }
}