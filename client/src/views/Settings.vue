<template>
  <div class="settings">
    <form>
      <div>
        <h2>Basic Info</h2>
        <div>
          <label for="firstName">First Name: </label>
          <input type="text" name="firstName" id="firstName" :value="caretaker.firstName">
        </div>
        <div>
          <label for="lastName">Last Name: </label>
          <input type="text" name="lastName" id="lastName" :value="caretaker.lastName">
        </div>
        <div>
          <label for="email">Email Address: </label>
          <input type="text" name="email" id="email" :value="caretaker.emailAddress">
        </div>
        <div>
          <label for="phoneNumber">Phone Number: </label>
          <input type="tel" name="phoneNumber" id="phoneNumber" :value="caretaker.phoneNumber">
        </div>
      </div>
      <div>
        
      </div>
    </form>
  </div>
</template>

<script>
import caretakerService from '../services/CaretakerService'

export default {
  data() {
    return {
      caretaker: {}
    }
  },
  created() {
    caretakerService.getCaretaker()
      .then(res => {
        if (res.status === 200) {
          this.caretaker = res.data;
        }
      })
      .catch(err => {
        console.log(err);
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