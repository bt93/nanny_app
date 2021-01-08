<template>
    <v-container>
        <error v-if="error"/>
        <v-card
            elevation="2"
            v-else
        >
            <v-card-title>Add Child</v-card-title>
            <v-form
                @submit.prevent="addChild"
                ref="form"
                class="px-12"
            >
                <v-row>
                    <v-col>
                        <v-text-field 
                            label="First Name"
                            v-model="child.firstName"
                            id="firstName"
                            :rules="nameRules"
                        /> 
                    </v-col>
                    <v-col>
                        <v-text-field 
                            label="Last Name"
                            v-model="child.lastName"
                            id="lastName"
                            :rules="nameRules"
                        />
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <v-select 
                            v-model="child.gender"
                            label="Gender"
                            :items="genders"
                            item-text="item"
                            item-value="value"
                        />
                    </v-col>
                    <v-col>
                        <v-text-field 
                            type="date"
                            label="Date of Birth"
                            v-model="child.dateOfBirth"
                            id="dateOfBirth"
                        />
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <v-text-field 
                            type="number"
                            min="0"
                            step="any"
                            label="Rate Per Hour (in U.S. Dollars)"
                            v-model="child.ratePerHour"
                            :rules="rateRules"
                        />
                    </v-col>
                    <v-col>
                        <v-radio-group
                            v-model="child.needsDiapers"
                            label="Needs Diaper?"
                            row
                            :rules="diaperRules"
                        >
                            <v-radio 
                                label="Yes"
                                :value="true"
                            />
                            <v-radio 
                                label="No"
                                :value="false"
                            />
                        </v-radio-group>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <upload-photo @image-upload="imageUpload" />
                    </v-col>
                </v-row>
                <v-row justify="center" v-if="isLoading" class="py-8">
                    <v-progress-circular 
                        indeterminate
                        color="primary"
                    />
                </v-row>
                <v-row class="py-12" v-else/>
                <v-row justify="center" class="pb-5">
                    <v-btn type="submit" class="mr-6">Submit</v-btn>
                    <v-btn :to="{name: 'dashboard'}">Cancel</v-btn>
                </v-row>
            </v-form>
        </v-card>
    </v-container>
</template>

<script>
import childrenService from '@/services/ChildrenService'
import Error from '@/components/Error'
import UploadPhoto from '@/components/UploadPhoto.vue'

export default {
    components: {
        Error,
        UploadPhoto
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
            genders: [
                { item: 'Female', value: 'F' },
                { item: 'Male', value: 'M' },
                { item: 'Non-Binary', value: 'N' },
                { item: 'Other', value: 'O' }
            ],
            error: false,
            isLoading: false
        }
    },
    methods: {
        addChild() {
            this.child.ratePerHour = Number(this.child.ratePerHour);
            this.isLoading = true;
            childrenService.addChild(this.child)
                .then(res => {
                    if (res.status === 201) {
                        this.$router.push({name: 'viewChild', params: {id: res.data.childId}})
                    }
                })
                .catch(err => {
                    if (err) {
                        this.isLoading = false;
                        this.error = true;
                    }
                });
        },
        imageUpload(value) {
            this.child.imageUrl = value;
        }
    }
}
</script>

<style>

</style>