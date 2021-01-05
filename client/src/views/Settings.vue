<template>
  <v-container class="settings">
    <img src="../images/loading.gif" alt="" v-if="isLoading">
    <Error v-else-if="error" />
    <v-container v-else>
      <v-form>
        <v-container class="d-md-flex justify-space-around">
          <v-card elevation="2" class="px-6">
            <h2>Basic Info</h2>
            <v-row>
              <v-col>
                <v-text-field 
                  label="First Name"
                  id="firstName"
                  v-model="caretaker.firstName"
                />
              </v-col>
              <v-col>
                <v-text-field 
                  label="Last Name"
                  id="lastName"
                  v-model="caretaker.lastName"
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <v-text-field 
                  label="Email Address"
                  id="emailAddress"
                  v-model="caretaker.emailAddress"
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <v-text-field 
                  label="Phone Number"
                  type="tel"
                  id="phoneNumber"
                  v-model="caretaker.phoneNumber"
                />
              </v-col>
            </v-row>
          </v-card>
          <v-card elevation="2" class="px-6">
            <h2>Address</h2>
            <v-row>
              <v-col>
                <v-text-field 
                  label="Street"
                  id="street"
                  v-model="caretaker.address.street"
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <v-text-field 
                  label="City"
                  id="city"
                  v-model="caretaker.address.city"
                />
              </v-col>
              <v-col>
                <v-text-field 
                  label="state"
                  id="state"
                  v-model="caretaker.address.state"
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <v-text-field 
                  label="Zip Code"
                  id="zip"
                  v-model="caretaker.address.zip"
                />
              </v-col>
              <v-col>
                <v-text-field 
                  label="County"
                  id="county"
                  v-model="caretaker.address.county"
                />
              </v-col>
            </v-row>
          </v-card>
        </v-container>
      </v-form>
    </v-container>
  </v-container>
</template>

<script>
import caretakerService from '../services/CaretakerService'
import Error from '../components/Error'

export default {
  components: {
    Error
  },
  data() {
    return {
      caretaker: {},
      isLoading: true,
      error: false
    }
  },
  methods: {
    submit() {
      caretakerService.updateCaretaker(this.caretaker)
        .then(res => {
          if (res.status === 201) {
            this.$router.push('/dashboard');
          }
        })
        .catch(err => {
          console.log(err);
        }); 
    }    
  },
  created() {
    caretakerService.getCaretaker()
      .then(res => {
        if (res.status === 200) {
          this.caretaker = res.data;
          this.isLoading = false;
        }
      })
      .catch(err => {
        console.log(err);
        this.isLoading = false;
        this.error = true;
      })
  }
}
</script>

<style>
.settings{
  display: block;
}

div {
  margin-bottom: 10px;
}
</style>