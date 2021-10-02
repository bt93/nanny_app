<template>
    <v-card class="pa-3" width="20em">
        <v-card-title >
            {{child.firstName}} {{ child.lastName }}
            <v-spacer />
            <v-avatar
                size="90"
                v-if="child.imageUrl"
            >
                <v-img
                    :src="child.imageUrl"
                    :alt="child.firstName"
                />
            </v-avatar>
            <v-avatar
                size="90"
                color="primary"
                v-else
            >
                <v-icon dark large>
                    mdi-account-circle
                </v-icon>
            </v-avatar>
        </v-card-title>
        <v-card-text>Date of Birth: {{ formatDOB }}</v-card-text>
        <v-card-text>Gender: {{ getGender }}</v-card-text>
        <v-card-text>Rate: ${{ child.ratePerHour }}</v-card-text>
        <v-card-text>Needs Diapers: {{ getNeedsDiapers }}</v-card-text>
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