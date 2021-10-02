<template>
    <v-card class="ma-12 px-12">
        <v-card-title>Change Password</v-card-title>
        <v-form @submit.prevent="submit" ref="form">
            <v-row>
              <v-text-field 
                label="Password"
                id="password"
                v-model="newPassword"
                type="password"
                :rules="passwordRules"
                />  
            </v-row>
            <v-row>
                <v-text-field 
                label="Confirm Password"
                id="confirmPassword"
                v-model="confirmPassword"
                type="password"
                :rules="confirmPasswordRules"
                />  
            </v-row>
            <v-row justify="center" class="py-4">
                <v-btn type="submit" class="mx-9" color="error">Submit</v-btn>
                <v-btn :to="{name: 'settings'}">Cancel</v-btn>
            </v-row>
        </v-form>
    </v-card>
</template>

<script>
import caretakerService from '../services/CaretakerService'

export default {
    data() {
        return {
            newPassword: '',
            confirmPassword: '',
            errorMessage: '',
            caretaker: {},
            passwordRules: [
                v => !!v || 'Password is required'
            ],
            confirmPasswordRules: [
                v => !!v || 'Confirm Password is required',
                v => v === this.newPassword || 'Password must match'
            ],
        }
    },
    methods: {
        submit() {
            if (this.$refs.form.validate()) {
                this.caretaker.password = this.newPassword;
                caretakerService.updatePassword(this.caretaker)
                    .then(res => {
                        if (res.status === 204) {
                            this.$router.push('/settings');
                        }
                    })
            }
                
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