<template>
  <vue-dropzone 
    id="dropzone"
    class="mt-3"
    v-bind:options="dropzoneOptions"
    v-on:vdropzone-sending="addFormData"
    v-on:vdropzone-success="getSuccess"
    :useCustomDropzoneOptions="true"
  ></vue-dropzone>
</template>

<script>
import vue2Dropzone from "vue2-dropzone";
import "vue2-dropzone/dist/vue2Dropzone.min.css";


export default {
    components: {
        vueDropzone: vue2Dropzone
    },
    data() {
        return {
                dropzoneOptions: {
                url: this.$store.state.cloudinaryUploadUrl,  
                thumbnailWidth: 250,
                thumbnailHeight: 250,
                maxFilesize: 2.0,
                acceptedFiles: ".jpg, .jpeg, .png, .gif",
                uploadMultiple: false,
                addRemoveLinks: true,
                dictDefaultMessage: 'Drop files here to upload. </br> Alternatively, click to select a file for upload.',                
            },
        }
    },
    methods: {
        addFormData(file, xhr, formData) {
            formData.append('api_key', this.$store.state.cloudinaryAPIKey.toString());
            formData.append('upload_preset', this.$store.state.cloudinaryUploadPreset.toString());
            formData.append('timestamp', (Date.now() / 1000) | 0);
            formData.append('tags', 'vue-app');
        },
        getSuccess(file, res) {
            const imageUrl = res.secure_url;
            this.$emit('image-upload', imageUrl);
        }
    }
}
</script>

<style>

</style>