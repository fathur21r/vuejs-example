<template>
    <div class="container">
        <Alert :Msg="msg" :Show="alert"></Alert>
        <PageHeader Title="Edit Category"></PageHeader>
        <div class="row">
            <div class="col-md-6 offset-md-3">
                <form class="needs-validation" novalidate @submit="Save">
                    <div class="row mb-3">
                        <label for="TxtId" class="col-sm-3 col-form-label">Id</label>
                        <div class="col-sm-9">
                            <input type="text" readonly class="form-control-plaintext" id="TxtId" v-model="txtId">
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label for="TxtName" class="col-sm-3 col-form-label">Category Name</label>
                        <div class="col-sm-9">
                            <input type="text" class="form-control" value="email@example.com" v-model="txtName" required>
                            <div class="invalid-feedback">
                                Please fill Category Name.
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label for="staticEmail" class="col-sm-3 col-form-label">Active</label>
                        <div class="col-sm-9">
                            <div class="custom-control custom-radio custom-control-inline">
                                <input type="radio" name="radioInline" class="custom-control-input" id="1" value="1" v-model="rdIsActive">
                                <label class="custom-control-label" for="1">Active</label>
                            </div>
                            <div class="custom-control custom-radio custom-control-inline">
                                <input type="radio" name="radioInline" class="custom-control-input" id="0" value="0" v-model="rdIsActive">
                                <label class="custom-control-label" for="0">Not Active</label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <button class="btn btn-md btn-secondary form-control" @click="Back">Back</button>
                        </div>
                        <div class="col-md-6">
                            <button class="btn btn-md btn-primary form-control">Save</button>
                        </div>
                    </div>
                </form>
            </div>
            <div class="col-md-3"></div>
        </div>        
    </div>
</template>
<script>
import PageHeader from "@/components/PageHeader.vue";
import Alert from "@/components/Alert.vue";

export default {
    name: "EditCategory",
    components: {
        PageHeader,
        Alert,
    },
    data: function () {
        return {
            alert: false,
            msg: "",
            txtId: '',
            txtName: '',
            rdIsActive: '1'
        };
    },
    created: async function () {
        this.txtId = this.$route.params.id
        await this.FetchData();
    },
    methods: {
        Back: function(){
            this.$router.push('/post')
        },
        FetchData: function () {
            this.$axios.get("/api/category/"+this.txtId)
            .then((res) => {
                if (res.data.status === "OK") {
                    this.txtName = res.data.category.name
                    this.rdIsActive = res.data.category.isActive ? '1' : '0'
                }
                else{
                    this.alertMsg = res.data.message
                    this.alert = true
                }
            })
            .catch((error) => {
                this.alertMsg = error.message
                this.alert = true
            });
        },
        Save: function(event){
            event.preventDefault();
            event.target.classList.add('was-validated');

            if(this.CategoryNameInsert !== ''){
                this.$axios
                .post("/api/category/save", {
                    id: this.txtId,
                    name: this.txtName,
                    isactive: this.rdIsActive,
                })
                .then((res) => {
                    if (res.data.status === "OK") {
                        alert('Update Success')
                        this.Back();
                    }
                    else{
                        this.alertInserMsg = res.data.message
                        this.alertInsert = true
                    }
                })
                .catch((error) => {
                    this.alertInserMsg = error.message
                    this.alertInsert = true
                });
            }
        },
    }
}
</script>
