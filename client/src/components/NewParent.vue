<template>
  <div class="newParent text-center">
      <form @submit.prevent="createParent">
            <h2>Basic Info</h2>
            <label for="firstName">First Name: </label>
            <input type="text" name="firstName" id="firstName" v-model="parent.firstName" placeholder="John">
            <label for="lastName">Last Name: </label>
            <input type="text" name="lastName" id="lastName" v-model="parent.lastName" placeholder="Doe">
            <label for="emailAddress">Email Address: </label>
            <input type="emailAddress" name="emailAddress" id="emailAddress" v-model="parent.emailAddress" placeholder="johndoe@example.com">
            <label for="phoneNumber">Phone Number: </label>
            <input type="tel" name="phoneNumber" id="phoneNumber" v-model="parent.phoneNumber" placeholder="xxxxxxxxxx">
            <h2>Address</h2>
            <label for="street">Street: </label>
            <input type="text" name="street" id="street" v-model="parent.address.street" placeholder="1234 Example Street">
            <label for="city">City: </label>
            <input type="text" name="city" id="city" v-model="parent.address.city" placeholder="Macedonia">
            <label for="state">State: </label>
            <input type="text" name="state" id="state" v-model="parent.address.state" placeholder="Ohio">
            <label for="zip">Zip Code: </label>
            <input type="number" name="zip" id="zip" min="0" max="99999" v-model="parent.address.zip" placeholder="xxxxx">
            <label for="county">County: </label>
            <input type="text" name="county" id="county" v-model="parent.address.county" placeholder="Summit">
            <label for="country">Country: </label>
            <select name="country" id="country" v-model="parent.address.country">
                <option value="" disabled selected="selected">Choose One</option>
                <option v-for="co in countries" :key="co" :value="co">{{ co }}</option>
            </select>
            <input type="submit" value="Submit">
      </form>
  </div>
</template>

<script>
import countryList from 'country-list';
import parentService from '../services/ParentService'

export default {
    name: 'new-parent',
    data() {
        return {
            parent: {
                address: {}
            },
            countries: countryList.getNames(),
            error: false
        }
    },
    methods: {
        createParent() {
            parentService.addParent(this.parent, this.$route.params.id)
                .then(res => {
                    if (res.status === 201) {
                        this.$router.push({name: 'viewChild', params: {id: this.$route.params.id}})
                    }
                })
                .catch(err => {
                    if (err) {
                        this.error = true;
                        console.log(err);
                    }
                });
        }
    }
}
</script>

<style>

</style>