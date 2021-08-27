
<template>
  <div class="container">
      <Alert :Msg="alertMsg" :Show="alert"></Alert>
      <PageHeader Title="Category"></PageHeader>

    <!-- Modal Insert -->
    <div class="row mb-3">
      <div class="col-md">
        <button class="btn btn-primary m-0" @click="OpenModalInsert">Create Category</button>
        <mdb-modal :show="ModalInsert" @close="CloseModalInsert" centered size="lg">
          <mdb-modal-header>
            <mdb-modal-title class="text-center">Insert Category</mdb-modal-title>
          </mdb-modal-header>
          <mdb-modal-body>
            <form class="needs-validation" novalidate @submit="Insert">
              <Alert :Msg="alertInserMsg" :Show="alertInsert"></Alert>
              <div class="form-row">
                <div class="col-md mb-3">
                  <label for="TxtName">Category Name</label>
                  <input type="text" class="form-control" id="TxtName" placeholder="Category Name" required v-model="CategoryNameInsert">
                  <div class="invalid-feedback">
                      Please fill Category Name.
                  </div>
                </div>
              </div>
              <div class="form-row">
                <div class="col-md mb-3">
                  <label for="TxtName">Is Active</label><br>
                  <div class="custom-control custom-radio custom-control-inline">
                      <input type="radio" name="radioInline" class="custom-control-input" id="1" value="1" v-model="IsActiveInsert">
                      <label class="custom-control-label" for="1">Active</label>
                  </div>
                  <div class="custom-control custom-radio custom-control-inline">
                      <input type="radio" name="radioInline" class="custom-control-input" id="0" value="0" v-model="IsActiveInsert">
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

    <!-- Card Search Data -->
    <div class="row mb-3">
      <div class="col-md">
        <div class="card">
          <div class="card-body">
            <div class="card-text">
              <form>
                <div class="row">
                  <div class="col-sm-3">
                    <input
                      type="text"
                      class="form-control"
                      id="TxtName"
                      placeholder="Category Name"
                      v-model="CategoryName"
                    />
                  </div>
                  <div class="col-sm-3">
                    <select class="form-control" id="" v-model="IsActive">
                        <option v-for="d in isActiveArr" :key="d.id" :value="d.value">{{d.text}}</option>
                    </select>
                  </div>
                  <div class="col-sm-6">
                    <mdb-btn
                      size="md"
                      class="my-0"
                      color="primary"
                      @click="Search"
                      >Search</mdb-btn
                    >
                  </div>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Grid View -->
    <div class="row">
      <div class="col-lg">
        <div class="table-responsive">
          <table class="table table-bordered table-striped align-middle table-hover">
            <thead class="table-secondary text-center">
              <tr>
                <th style="font-weight: bold; width: 5%">Id</th>
                <th style="font-weight: bold; width: 40%">Name</th>
                <th style="font-weight: bold; width: 5%">Active</th>
                <th style="font-weight: bold; width: 15%">Created Date</th>
                <th style="font-weight: bold; width: 15%">
                  Last Modified Date
                </th>
                <th style="font-weight: bold; width: 20%">Action</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="cat in data" :key="cat.id">
                <td class="text-center">
                  {{ cat.id }}
                </td>
                <td>
                  {{ cat.name }}
                </td>
                <td class="text-center">
                  {{ cat.isActive }}
                </td>
                <td class="text-center">
                  {{ cat.createdDate }}
                </td>
                <td class="text-center">
                  {{ cat.lastModifiedDate }}
                </td>
                <td class="text-center">
                  <mdb-btn size="sm" color="primary" @click="Edit(cat.id)"><i class="fas fa-pencil-alt"></i></mdb-btn>
                  <mdb-btn size="sm" color="danger" @click="Delete(cat.id)"><i class="fas fa-trash-alt"></i></mdb-btn>
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
  name: "Category",
  components: {
    PageHeader,
    Alert,
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
        text: 'Active Category'
      },{
        id: 2,
        value: '0',
        text: 'Non Active Category'
      }],
      alert: false,
      alertMsg: "",
      abc: "",
      CategoryName: "",
      IsActive: "",
      data: [],
      ModalInsert: false,
      CategoryNameInsert: '',
      IsActiveInsert: '1',
      alertInsert: false,
      alertInserMsg: ''
    };
  },
  created: async function () {
    await this.FetchData();
  },
  methods: {
    FetchData: function () {
      this.$axios
        .post("/api/category", {
          name: this.CategoryName,
          isactive: this.IsActive,
        })
        .then((res) => {
          if (res.data.status === "OK") {
            this.data = res.data.data;
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
    Search: function () {
      this.FetchData();
    },
    OpenModalInsert: function(){
      this.ModalInsert = true;
    },
    CloseModalInsert: function(){
      this.CategoryNameInsert = ''
      this.IsActiveInsert = '1'
      this.ModalInsert = false;
    },
    Insert: function(event){
      event.preventDefault();
      event.target.classList.add('was-validated');

      if(this.CategoryNameInsert !== ''){
        this.$axios
          .post("/api/category/save", {
            name: this.CategoryNameInsert,
            isactive: this.IsActiveInsert,
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
      this.$router.push('/category/' + id)
    },
    Delete: function(id){
      this.$axios
        .post("/api/category/delete", {
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
