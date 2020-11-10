<template>
  <div class="settings">
    <img src="../images/loading.gif" alt="" v-if="isLoading">
    <Error v-else-if="error" />
    <form v-else @submit.prevent="submit">
      <div class="input">
        <h2>Basic Info</h2>
        <div>
          <label for="firstName">First Name: </label>
          <input type="text" name="firstName" id="firstName" v-model="caretaker.firstName">
        </div>
        <div>
          <label for="lastName">Last Name: </label>
          <input type="text" name="lastName" id="lastName" v-model="caretaker.lastName">
        </div>
        <div>
          <label for="email">Email Address: </label>
          <input type="text" name="email" id="email" v-model="caretaker.emailAddress">
        </div>
        <div>
          <label for="phoneNumber">Phone Number: </label>
          <input type="tel" name="phoneNumber" id="phoneNumber" v-model="caretaker.phoneNumber">
        </div>
      </div>
      <div class="input">
        <h2>Address</h2>
        <div>
          <label for="street">Street: </label>
          <input type="address" name="street" id="street" v-model="caretaker.address.street">
        </div>
        <div>
          <label for="city">City: </label>
          <input type="city" name="city" id="city" v-model="caretaker.address.city">
        </div>
        <div>
          <label for="state">State: </label>
          <input type="state" name="state" id="state" v-model="caretaker.address.state">
        </div>
        <div>
          <label for="zip">Zip Code: </label>
          <input type="zip" name="zip" id="zip" v-model="caretaker.address.zip">
        </div>
        <div>
          <label for="county">County: </label>
          <input type="text" name="county" id="county" v-model="caretaker.address.county">
        </div>
        <div>
          <label for="country">Country: </label>
          <input type="text" name="country" id="country" v-model="caretaker.address.country">
        </div>
      </div>
      <div>
          <router-link :to="{ name: 'changePassword' }">Change password</router-link>
      </div>
      <div>
        <input type="submit">
      </div>
    </form>
  </div>
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