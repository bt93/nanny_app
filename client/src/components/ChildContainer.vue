<template>
    <router-link :to="{name: 'viewChild', params: {id: child.childId, firstName: child.firstName.toLowerCase(), lastName: child.lastName.toLowerCase()}}">
        <div class="childContainer">
            <h3>{{ child.firstName }} {{ child.lastName }}</h3>
            <ul>
                <li>Date of Birth: {{ formatDOB }}</li>
                <li>Gender: {{ getGender }}</li>
                <li>Rate: ${{ child.ratePerHour }}</li>
                <li>Needs diapers: {{ getNeedsDiapers }}</li>
            </ul>
        </div>
    </router-link>
</template>

<script>
import moment from 'moment'

export default {
    name: 'child-container',
    props: {
        child: Object
    },
    computed: {
        formatDOB() {
            const day = new Date(this.child.dateOfBirth);
            
            return moment(day).format("l")
        },
        getGender() {
            if (this.child.gender === 'F') {
                return 'Female';
            } else if (this.child.gender === 'M') {
                return 'Male'
            } else if (this.child.gender === 'N') {
                return 'Non-Binary'
            }
            
            return 'Other'
        },
        getNeedsDiapers() {
            if (this.child.needsDiapers) {
                return 'Yes'
            } else {
                return 'No'
            }
        }
    },
}
</script>

<style>
.childContainer {
    border: rgb(58, 81, 102) 2px solid;
    border-radius: 20px;
    margin-bottom: 20px;
    margin-right: 20px;
}

.childContainer:hover {
  background-color: rgb(37, 57, 73);
  color: rgb(72, 223, 185);
}

ul {
    list-style: none;
    padding: 0;
}
</style>