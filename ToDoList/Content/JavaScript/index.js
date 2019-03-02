var app = new Vue({
    el: '#toDoList',
    data: {
        newToDo: '',
        list: [
            {
                ID: '123',
                Title: '正在載入...',
                Completed: false,
                Color:'all'
            }
        ],
        clickedID: '',
        clickedTitle: '',
        clickedDetail: '',
        CardMode: false,
        ColorFilter: 'all'

    },
    methods: {
        addToDo: function () {
            var title = this.newToDo.trim();
            //var id = Math.floor(Math.random() * 1000);
            console.log('使用者新增：' + this.newToDo);
            if (!title) {
                return;
            }

            $.ajax({
                type: 'POST',
                url: '/Home/AddToDo',
                data: 'token=' + localStorage.token + '&Title=' + title,
                dataType: "JSON",
                error: function () { console.log('連線異常') },
                success: function (returnData) {
                    console.log(returnData);
                    if (returnData.Status == true) {
                        //window.location.reload();
                        app.list.push({
                            ID: returnData.ID,
                            Title: returnData.Title,
                            Completed: returnData.Completed,
                            Owner_ID: '12314234',
                            Detail: '',
                            Create_Date: null,
                            Completed_Date: null

                        });
                        app.newToDo = '';
                        console.log('新增成功');

                    }
                    else {
                        alert("新增失敗");

                    }
                }
            });





        },


        completed: function (key) {

            console.log('使用者勾選編號：' + key);


            $.ajax({
                type: 'POST',
                url: '/Home/UpdateToDoCompleted',
                data: 'token=' + localStorage.token + '&ID=' + key,
                dataType: "JSON",
                error: function () { console.log('連線異常') },
                success: function (returnData) {
                    console.log(returnData);
                    if (returnData.Status == true) {

                        console.log('更新成功');

                    }
                    else {
                        alert("更新失敗，請重新整理");

                    }
                }
            });

        },
        deleted: function (item) {

            console.log('使用者刪除編號：' + item.ID);
            var thisIndex = this.list.findIndex(function (listItem, key) {
                return item.ID === listItem.ID
            });

            $.ajax({
                type: 'POST',
                url: '/Home/UpdateToDoDeleted',
                data: 'token=' + localStorage.token + '&ID=' + item.ID,
                dataType: "JSON",
                error: function () { console.log('連線異常') },
                success: function (returnData) {
                    console.log(returnData);
                    if (returnData.Status == true) {
                        app.list[thisIndex].Deleted = true;
                        console.log('刪除成功');

                    }
                    else {
                        alert("刪除失敗，請重新整理");

                    }
                }
            });

        },

        edit: function (item) {
            console.log('雙擊:', item.ID);
            this.clickedID = item.ID;
            this.clickedTitle = item.Title;
            this.clickedDetail = item.Detail;

        },
        cancelEdit: function () {
            console.log('取消編輯');
            this.clickedID = '';
            this.clickedTitle = '';
            this.clickedDetail = '';
        },
        updateTitle: function (item) {
            console.log('使用者修改編號：' + this.clickedID + '之標題');
            var thisIndex = getThisIndex(item);
            //var thisIndex2 = this.list.findIndex(function (listItem, key) {
            //    return item.ID === listItem.ID
            //});
            //console.log('thisIndex2 is ' + thisIndex2);
            axios.post('/Home/UpdateToDoTitle', {
                token: localStorage.token,
                ID: this.clickedID,
                Title: this.clickedTitle
            })
                .then(function (response) {
                    console.log(response);
                    if (response.data.Status == true) {
                        app.list[thisIndex].Title = app.clickedTitle;
                        app.clickedID = '';
                        app.clickedTitle = '';
                        console.log('更新成功');

                    }
                    else {
                        alert("更新失敗");

                    }
                })
                .catch(function (error) {
                    console.log(error);
                })

            /*$.ajax({
                type: 'POST',
                url: '/Home/UpdateToDoTitle',
                data: 'token=' + localStorage.token + '&ID=' + this.clickedID +  '&Title='+this.clickedTitle,
                dataType: "JSON",
                error: function () { console.log('連線異常') },
                success: function (returnData) {
                    console.log(returnData);
                    if (returnData.Status == true) {
                        item.Title=this.clickedTitle;
                        app.clickedID='';
                        app.clickedTitle='';
                        app.clickedDetail='';
                        console.log('更新成功');
    
                    }
                    else {
                        alert("更新失敗");
    
                    }
                	
                }
            });*/
        },
        updateDetail: function (item) {
            console.log('使用者修改編號：' + this.clickedID + '之標題');
            var thisIndex = getThisIndex(item);
            axios.post('/Home/UpdateToDoDetail', {
                token: localStorage.token,
                ID: this.clickedID,
                Detail: this.clickedDetail
            })
                .then(function (response) {
                    console.log(response);
                    if (response.data.Status == true) {

                        app.list[thisIndex].Detail = app.clickedDetail;
                        app.clickedID = '';
                        app.clickedTitle = '';
                        app.clickedDetail = '';
                        console.log('更新成功');

                    }
                    else {
                        alert("更新失敗");

                    }
                })
                .catch(function (error) {
                    console.log(error);
                })
        }

    }
    ,
    computed: {
        FilterColor: function () {
            
            if (this.ColorFilter == 'all')
                return this.list
            else if (this.ColorFilter == 'primary') {
                if (this.list != null)
                    return getThisColorList('primary', this.list)
            }
            return [];
        }
    }
});


GetToDoList();
function GetToDoList() {
    console.log('拿代辦事項');
    $.ajax({
        type: 'POST',
        url: '/Home/GetToDoList',
        data: 'token=' + localStorage.token,
        dataType: "JSON",
        error: function () { console.log('連線異常') },
        success: function (returnData) {
            console.log(returnData);
            if (returnData.Status == true) {
                app.list = returnData.List;
                //document.location.href = "/home";

                console.log(returnData.List);
            }
            else {
                alert("登入驗證錯誤!");
                document.location.href = "/Login";
            }
        }
    });
}

function getThisIndex(item) {
    var thisIndex;
    app.list.some(function (el, i) {
        if (el.ID === item.ID) {
            console.log('thisIndex is ' + i)
            thisIndex = i;
        }
    });
    //console.log('getThisIndex  thisIndex is ' + thisIndex)
    return thisIndex;
}
function getThisColorList(color,list) {
    var ColorList = [];
    list.some(function (el, i) {
        if (el.Color === color) {
            console.log('index ' + i +' is '+color)
            ColorList.push(el);
        }
    });
    return ColorList;
}



