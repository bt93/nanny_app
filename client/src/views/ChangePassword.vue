<template>
  <div class="change-password">
      <h1>Change Password</h1>
      <h2 v-if="errorMessage">{{ errorMessage }}</h2>
      <form @submit.prevent="submit">
          <div>
            <label for="password">Password: </label>
            <input type="password" name="password" id="password" v-model="newPassword" required>
          </div>
          <div>
              <label for="confrimPassword">Confrim Password: </label>
              <input type="password" name="confirmPassword" id="confirmPassword" v-model="confirmPassword" required>
          </div>
          <input type="submit">
          <button @click.prevent="cancel">Cancel</button>
      </form>
  </div>
</template>

<script>
import caretakerService from '../services/CaretakerService'

export default {
    data() {
        return {
            newPassword: '',
            confirmPassword: '',
            errorMessage: '',
            caretaker: {}
        }
    },
    methods: {
        submit() {
            if (this.newPassword === this.confirmPassword) {
                this.caretaker.password = this.newPassword;
                caretakerService.updatePassword(this.caretaker)
                    .then(res => {
                        if (res.status === 204) {
                            this.$router.push('/settings');
                        }
                    })
            } else {
                this.errorMessage = 'Passwords do not match. Please try again.';
            }
        },
        cancel() {
            this.$router.push('/settings');
        }
    },
    created() {
        caretakerService.getCaretaker()
            .then(res => {
                if (res.status === 200) {
                    this.caretaker = res.data;
                }
            })
    }
}
</script>

<style>

</style>