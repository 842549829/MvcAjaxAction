let vm = new Vue({
    el: '#app',
    data: {
        // 数据
        items: [],
        // 分页信息
        paging: {
            pageSize: 10,
            pageIndex: 1,
            dataCount: 0,
            pageCount: 0
        }
    },
    // 局部组件
    components: {
        "table-template": (resolve, reject) => {
            $.get("/conetnt/template/tableTemplate.html").then(res =>
                resolve({
                    template: res,
                    props: ['items', "dataLength"]
                })
            );
        }
    },
    computed: {
        dataLength: function () {
            return this.items.length;
        },
        pageSizeArray: function () {
            let array = new Array();
            let index = this.paging.pageIndex;
            let size = this.paging.pageSize;
            for (let i = index - 4; i <= index + 4; i++) {
                if (i === index) {
                    array.push({ isFirst: true, value: i });
                } else if (i > 0 && i <= this.paging.pageCount) {
                    array.push({ isFirst: false, value: i });
                }
            }
            return array;
        }
    },
    mounted: function () {
        this.queryPaging(1, this.pageSize);
    },
    methods: {
        search: function (pageIndex) {
            this.queryPaging(pageIndex, this.pageSize);
        },
        getQueryPagination: function (pageIndex, pageSize) {
            return { "PageSize": pageSize, "PageIndex": pageIndex, "GetRowCount": true };
        },
        getCondtionModel: function () {
            let model = new Object();
            model.Address = $.trim($("#txtAddress").val());
            model.Name = $.trim($("#txtName").val());
            model.MOBile = $.trim($("#txtMobile").val());
            return model;
        },
        bindData: function (self, data) {
            let d = data.Person;
            self.items = [];
            self.paging.pageSize = data.Pagination.PageSize;
            self.paging.pageIndex = data.Pagination.PageIndex;
            self.paging.dataCount = data.Pagination.RowCount;
            self.paging.pageCount = parseInt((self.paging.dataCount + self.paging.pageSize - 1) / self.paging.pageSize);
            for (let i = 0; i < d.length; i++) {
                self.$set(vm.items, i, d[i]);
            }
        },
        queryPaging: function (pageIndex, pageSize) {
            let self = this;
            if (!pageIndex) {
                pageIndex = 1;
            }
            pageSize = pageSize || 10;
            let condition = new Object();
            condition.Person = this.getCondtionModel();
            condition.Pagination = this.getQueryPagination(pageIndex, pageSize);
            $.net.Default.GetParameters(condition, {
                success: (data) => {
                    self.bindData(self, data);
                }
            });
        }
    }
});