<template>
    <v-card 
        class="childContainer"
        elevation="5"
    >
        <h3>{{ child.firstName }} {{ child.lastName }}</h3>
        <ul>
            <li>Date of Birth: {{ formatDOB }}</li>
            <li>Gender: {{ getGender }}</li>
            <li>Rate: ${{ child.ratePerHour }}</li>
            <li>Needs diapers: {{ getNeedsDiapers }}</li>
        </ul>
        <v-btn :to="{name: 'viewChild', params: {id: child.childId}}">View {{ child.firstName }}</v-btn>
    </v-card>
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
</style>