<template>
  <div class="newChild text-center">
      <h1>New Child</h1>
      <error v-if="error" />
      <form @submit.prevent="addChild">
            <label for="firstName">First Name: </label>
            <input type="text" name="firstName" id="fistName" v-model="child.firstName" />
            <label for="lastName">Last Name: </label>
            <input type="text" name="lastName" id="flastName" v-model="child.lastName">
            <label for="gender">Gender: </label>
            <select name="gender" id="gender" v-model="child.gender">
                <option value="" disabled selected="selected">Choose One</option>
                <option value="F">Female</option>
                <option value="M">Male</option>
                <option value="N">Non-Binary</option>
                <option value="O">Other</option>
            </select>
            <label for="dateOfBirth">Date Of Birth: </label>
            <input type="date" name="dateOfBirth" id="dateOfBirth" v-model="child.dateOfBirth">
            <label for="ratePerHour">Rate Per Hour: </label>
            <input type="number" name="ratePerHour" id="ratePerHour" min="0" step="any" v-model="child.ratePerHour">
            <label for="needsDiapers">Needs Diapers? </label>
            <select name="needsDiapers" id="needsDiapers" v-model="child.needsDiapers">
                <option value="" disabled selected="selected">Choose One</option>
                <option :value="true">Yes</option>
                <option :value="false">No</option>
            </select>
            <input type="submit" value="Submit">
      </form>
  </div>
</template>

<script>
import childrenService from '@/services/ChildrenService'
import Error from '@/components/Error'

export default {
    components: {
        Error
    },
    data() {
        return {
            child: {
                firstName: '',
                lastName: '',
                gender: '',
                dateOfBirth: '',
                ratePerHour: '',
                needsDiapers: false,
                active: true,
                imageUrl: ''
            },
            error: false
        }
    },
    methods: {
        addChild() {
            this.child.ratePerHour = Number(this.child.ratePerHour);

            childrenService.addChild(this.child)
                .then(res => {
                    if (res.status === 201) {
                        this.$router.push({name: 'viewChild', params: {id: res.data.childId}})
                    }
                })
                .catch(err => {
                    if (err) {
                        this.error = true;
                    }
                });
        }
    }
}
</script>

<style>

</style>