Vue.component("paging-template", resolve => {
    $.get("/conetnt/template/pagingTemplate.html").then(res =>
        resolve({
            template: res,
            props: ['array', "paging"],
            methods: {
                fatherFun: item => {
                    if (vm.search) {
                        vm.search(item);
                    }
                }
            }
        })
    );
});