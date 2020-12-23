<template>
    <v-card class="mx-auto px-12 py-3" min-width="15vw" max-width="40vw">
        <v-card-title>
            {{child.firstName}} {{ child.lastName }}
            <v-spacer />
                    <v-avatar>
            
            </v-avatar>
        </v-card-title>
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