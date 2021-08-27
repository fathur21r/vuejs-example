
<template>
  <div class="container">
      <Alert :Msg="alertMsg" :Show="alert"></Alert>
      <PageHeader Title="Post"></PageHeader>

  <!-- Modal Insert -->
    <div class="row mb-3">
      <div class="col-md">
        <button class="btn btn-primary m-0" @click="OpenModalInsert">Create Post</button>
        <mdb-modal :show="ModalInsert" @close="CloseModalInsert" centered size="lg">
          <mdb-modal-header>
            <mdb-modal-title class="text-center">New Post</mdb-modal-title>
          </mdb-modal-header>
          <mdb-modal-body>
            <form class="needs-validation" novalidate @submit="Insert">
              <Alert :Msg="alertInserMsg" :Show="alertInsert"></Alert>
              <div class="form-row">
                <div class="col-md mb-3">
                  <label for="DdlCategory">Category</label>
                  <select class="form-control" id="DdlCategory" required v-model="DdlCategory">
                    <option value=""> -- SELECT CATEGORY -- </option>
                    <option v-for="d in CategoryArr" :key="d.id" :value="d.value">{{d.text}}</option>
                  </select>
                  <div class="invalid-feedback">
                      Please select category.
                  </div>
                </div>
              </div>
              <div class="form-row">                
                <div class="col-md mb-3">
                  <label for="TxtTitle">Title</label>
                  <input type="text" class="form-control" id="TxtTitle" placeholder="Title" required v-model="TxtTitle">
                  <div class="invalid-feedback">
                      Please fill title.
                  </div>
                </div>
              </div>
              <div class="form-row">                
                <div class="col-md mb-3">
                  <label for="TxtContent">Content</label>
                  <textarea class="form-control" id="TxtContent" rows="3" placeholder="Content" required v-model="TxtContent"></textarea>
                  <div class="invalid-feedback">
                      Please fill content.
                  </div>
                </div>
              </div>
              <div class="form-row">
                <div class="col-md mb-3">
                  <label for="TxtName">Is Active</label><br>
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
              <button type="submit" class="btn btn-primary m-0">Submit</button>
            </form>
          </mdb-modal-body>
        </mdb-modal>
      </div>
    </div>

    <!-- Card Search -->
    <div class="row mb-3">
      <div class="col-md">
        <div class="card">
          <div class="card-body">
            <div class="card-text">
              <form @submit="Search">
                <div class="row">
                  <div class="col-sm-3">
                    <select class="form-control" id="" v-model="CategoryId">
                      <option value=""> -- ALL CATEGORY -- </option>
                        <option v-for="d in CategoryArr" :key="d.id" :value="d.value">{{d.text}}</option>
                    </select>
                  </div>
                  <div class="col-sm-3">
                    <select class="form-control" id="" v-model="IsActive">
                        <option v-for="d in isActiveArr" :key="d.id" :value="d.value">{{d.text}}</option>
                    </select>
                  </div>
                  <div class="col-sm-6">
                    <button class="btn btn-md btn-primary my-0" type="submit">Search</button>
                  </div>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Grid -->
    <div class="row">
      <div class="col-lg">
        <div class="table-responsive">
          <table
            class="table table-bordered table-striped align-middle table-hover"
          >
            <thead class="table-secondary text-center">
              <tr>
                <th style="font-weight: bold; width: 4%">Id</th>
                <th style="font-weight: bold; width: 10%">Category</th>
                <th style="font-weight: bold; width: 11%">Title</th>
                <th style="font-weight: bold; width: 39%">Content</th>
                <th style="font-weight: bold; width: 5%">Active</th>
                <th style="font-weight: bold; width: 15%">Posting Date</th>
                <th style="font-weight: bold; width: 16%">Action</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="d in data" :key="d.id">
                <td class="text-center">
                  {{ d.id }}
                </td>
                <td class="text-center">
                  {{ d.category }}
                </td>
                <td class="text-center">
                  {{ d.title }}
                </td>
                <td class="text-justify">
                  {{ d.content.substring(0,300) + '...' }}
                </td>
                <td class="text-center">
                  {{ d.isActive }}
                </td>
                <td class="text-center">
                  {{ d.postingDate }}
                </td>
                <td class="text-center">
                  <button class="btn btn-sm btn-primary" @click="Edit(d.id)"><i class="fas fa-pencil-alt"></i></button>
                  <button class="btn btn-sm btn-danger" @click="Delete(d.id)"><i class="fas fa-trash-alt"></i></button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import PageHeader from "@/components/PageHeader.vue";
import Alert from "@/components/Alert.vue";

export default {
  name: "Post",
  components: {
    PageHeader,
    Alert
  },
  data: function () {
    return {
      isActiveArr:[{
          id: 0,
          value: '',
          text: 'All Active State'
        },{
          id: 1,
          value: '1',
          text: 'Active Post'
        },{
          id: 3,
          value: '0',
          text: 'Non Active Post'
        }
      ],
      CategoryArr: [],

      alert: false,
      alertMsg: "",
      CategoryId: "",
      IsActive: "",
      data: [],

      ModalInsert: false,
      alertInsert: false,
      alertInserMsg: '',

      TxtTitle: '',
      DdlCategory: '',
      TxtContent: '',
      DdlIsActive: '1'
    };
  },
  created: async function () {
    await this.FetchCategory();
    await this.FetchData();    
  },
  methods: {
    FetchCategory: async function(){
      await this.$axios
        .post("/api/category", {
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
    FetchData: function () {
      this.$axios
        .post("/api/post", {
          categoryid: this.CategoryId,
          isactive: this.IsActive,
        })
        .then((res) => {
          if (res.data.status === "OK") {
            this.data = res.data.data;
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
    Search: function (e) {
      e.preventDefault();
      this.FetchData();
    },
    OpenModalInsert: function(){
      this.ModalInsert = true;
    },
    CloseModalInsert: function(){
      this.TxtTitle = ''
      this.DdlCategory = ''
      this.TxtContent = ''
      this.DdlIsActive = '1'
      this.ModalInsert = false;
    },
    Insert: function(event){
      event.preventDefault();
      event.target.classList.add('was-validated');

      if(this.DdlCategory !== '' && this.TxtTitle !== '' && this.TxtContent !== ''){
        this.$axios
          .post("/api/post/save", {
            categoryId: this.DdlCategory,
            content: this.TxtContent,
            title: this.TxtTitle,
            isActive: this.DdlIsActive
          })
          .then((res) => {
            if (res.data.status === "OK") {
              alert('Insert Success')
              this.FetchData()
              this.CloseModalInsert()
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
    Edit: function(id){
      this.$router.push('/post/' + id)
    },
    Delete: function(id){
      this.$axios
        .post("/api/post/delete", {
          id: id
        })
        .then((res) => {
          if (res.data.status === "OK") {
            alert('Delete Success')
            this.FetchData()
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
    }
  },
};
</script>
