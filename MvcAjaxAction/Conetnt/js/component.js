Vue.component("parent-template", (resolve, reject) => {
    $.get("/conetnt/template/pagingTemplate.html").then(res =>
        resolve({
            template: res,
            props: ['array', "paging"],
            methods: {
                fatherFun: function (item) {
                    if (vm.search) {
                        vm.search(item);
                    }
                }
            }
        })
    );
});