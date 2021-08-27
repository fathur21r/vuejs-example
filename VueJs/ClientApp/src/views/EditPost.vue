<template>
    <div class="container">
        <Alert :Msg="msg" :Show="alert"></Alert>
        <PageHeader Title="Edit Post"></PageHeader>
        <div class="row">
            <div class="col-md-6 offset-md-3">
                <form class="needs-validation" novalidate @submit="Save">
                    <div class="row mb-3">
                        <label for="TxtId" class="col-sm-3 col-form-label">Id</label>
                        <div class="col-sm-9">
                            <input type="text" readonly class="form-control-plaintext" id="TxtId" v-model="TxtId">
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label for="DdlCategory" class="col-sm-3 col-form-label">Category Name</label>
                        <div class="col-sm-9">
                            <select class="form-control" id="DdlCategory" required v-model="DdlCategory">
                                <option value=""> -- SELECT CATEGORY -- </option>
                                <option v-for="d in CategoryArr" :key="d.id" :value="d.value">{{d.text}}</option>
                            </select>
                            <div class="invalid-feedback">
                                Please select category.
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label for="TxtTitle" class="col-sm-3 col-form-label">Title</label>
                        <div class="col-sm-9">
                            <input type="text" class="form-control" id="TxtTitle" placeholder="Title" required v-model="TxtTitle">
                            <div class="invalid-feedback">
                                Please fill title.
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label for="TxtContent" class="col-sm-3 col-form-label">Content</label>
                        <div class="col-sm-9">
                            <textarea class="form-control" id="TxtContent" rows="10" placeholder="Content" required v-model="TxtContent"></textarea>
                            <div class="invalid-feedback">
                                Please fill content.
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label for="staticEmail" class="col-sm-3 col-form-label">Active</label>
                        <div class="col-sm-9">
                            <div class="custom-control custom-radio custom-control-inline">
                                <input type="radio" name="radioInline" class="custom-control-input" id="1" value="1" v-model="DdlIsActive">
                                <label class="custom-control-label" for="1">Active</label>
                            </div>
                            <div class="custom-control custom-radio custom-control-inline">
                                <input type="radio" name="radioInline" class="custom-control-input" id="0" value="0" v-model="DdlIsActive">
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
            CategoryArr: [],
            alert: false,
            msg: "",
            TxtId: "",
            TxtTitle: '',
            DdlCategory: '',
            TxtContent: '',
            DdlIsActive: '1'
        };
    },
    created: async function () {
        this.TxtId = this.$route.params.id
        await this.FetchData();
        await this.FetchCategory();
    },
    methods: {
        FetchCategory: async function(){
            await this.$axios.post("/api/category", {
                name: '',
                isactive: '1',
            })
            .then((res) => {
            if (res.data.status === "OK") {
                res.data.data.map((e, i)=>{
                    this.CategoryArr.push({
                        id: e.id,
                        value: e.id,
                        text: e.name
                    })
                });
            }
            else{
                this.alertMsg += "\r\n" + res.data.message
                this.alert = true
            }
            })
            .catch((error) => {
                this.alertMsg += "\r\n" + error.message
                this.alert = true
            });
        },
        Back: function(){
            this.$router.push('/post')
        },
        FetchData: function () {
            this.$axios.get("/api/post/"+this.TxtId)
            .then((res) => {
                if (res.data.status === "OK") {
                    this.txtName = res.data.post.name
                    this.DdlCategory = res.data.post.categoryId
                    this.TxtTitle = res.data.post.title
                    this.TxtContent = res.data.post.content
                    this.DdlIsActive = res.data.post.isActive ? '1' : '0'
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
            if(this.DdlCategory !== '' && this.TxtTitle !== '' && this.TxtContent !== ''){
                this.$axios
                .post("/api/post/save", {
                    id: this.TxtId,
                    categoryId: this.DdlCategory,
                    content: this.TxtContent,
                    title: this.TxtTitle,
                    isActive: this.DdlIsActive
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
