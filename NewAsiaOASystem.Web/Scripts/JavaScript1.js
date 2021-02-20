
//流动儿童接口返回值格式
var json = {
    "ChildrenInfo": [
        {
            "ChildId": "20152320", "ChildName": "张三", "ChildSex": "男",
            "ChildBirth": "2014-11-24", "ChildIdNumber": "654001201411243327",
            "ChildNational": "汉族", "ChildCommunity": "红塔山社区",
            "ChildAddress": "新疆农七师奎屯", "ChildRegistrationDate": "2015-04-27",
            "RelativeInfo": [
             {
                 "RelativeId": "20152322", "RelativeName": "张强", "RelativeSex": "男",
                 "RelativeNumber": "654001201411243327", "RelativeNational": "汉族",
                 "RelativePhone": "13565906954", "RelativeUnit": "",
                 "RelativeRegister": "新疆农七师奎屯126团13连112号",
                 "RelativeAddress": "新疆农七师奎屯", "RelativeType": "父亲"
             },
              {
                  "RelativeId": "20152321", "RelativeName": "李洁", "RelativeSex": "女",
                  "RelativeNumber": "654001201411243327", "ChildBirthId": "20152320001",
                  "RelativeNational": "汉族", "RelativePhone": "13565906954", "RelativeUnit": "",
                  "RelativeRegister": "新疆农七师奎屯126团13连112号",
                  "RelativeAddress": "新疆农七师奎屯", "RelativeType": "母亲"
              }
            ]

        },
        {
            "ChildId": "20152320", "ChildName": "李四", "ChildSex": "女",
            "ChildBirth": "2014-11-24", "ChildIdNumber": "654001201411243327", "ChildBirthId": "20152320001",
            "ChildNational": "汉族","ChildCommunity":"红塔山社区",
            "ChildAddress": "新疆农七师奎屯", "RelativeInfo": [], "Inoculate": []
        }
    ]
}




//已免疫儿童接口返回值格式
var json = {
    "ChildrenInfo": [
        {
            "ChildId": "20152320", "ChildName": "张三", "ChildSex": "男",
            "ChildBirth": "2014-11-24", "ChildIdNumber": "654001201411243327", "ChildBirthId": "20152320001",
            "ChildNational": "汉族", "ChildCensusRegister": "新疆农七师奎屯126团13连112号",
            "ChildCommunity":"红塔山社区","ChildAddress": "新疆农七师奎屯", "ChildBirthHospital": "出生医院",
            "ChildAllergicInfo": "过敏史", "ChildLastDate": "2015-04-27",
            "RelativeInfo": [
             {
                 "RelativeId": "20152322", "RelativeName": "张强", "RelativeSex": "男",
                 "RelativeNumber": "654001201411243327", "RelativeNational": "汉族",
                 "RelativePhone": "13565906954", "RelativeUnit": "",
                 "RelativeRegister": "新疆农七师奎屯126团13连112号",
                 "RelativeAddress": "新疆农七师奎屯", "RelativeType": "父亲"
             },
              {
                  "RelativeId": "20152321", "RelativeName": "李洁", "RelativeSex": "女",
                  "RelativeNumber": "654001201411243327", "ChildBirthId": "20152320001",
                  "RelativeNational": "汉族", "RelativePhone": "13565906954", "RelativeUnit": "",
                  "RelativeRegister": "新疆农七师奎屯126团13连112号",
                  "RelativeAddress": "新疆农七师奎屯", "RelativeType": "母亲"
              }
            ],
            "Inoculate": [
                   {
                       "InoculateId": "20152322", "InoculateName": "乙肝疫苗", "ChildUntowardEff": "不良反应",
                       "ChildTaboo": "禁忌症", "InoculateDate": "2015-04-27"
                   },
                   {
                       "InoculateId": "20152322", "InoculateName": "流感疫苗", "ChildUntowardEff": "不良反应",
                       "ChildTaboo": "禁忌症", "InoculateDate": "2015-04-27"
                   }
            ]


        },
        {
            "ChildId": "20152320", "ChildName": "李四", "ChildSex": "女",
            "ChildBirth": "2014-11-24", "ChildIdNumber": "654001201411243327", "ChildBirthId": "20152320001",
            "ChildNational": "汉族", "ChildCensusRegister": "新疆农七师奎屯100团130连11号",
            "ChildCommunity":"红塔山社区","ChildAddress": "新疆农七师奎屯", "RelativeInfo": [], "Inoculate": []
        }
    ],
}