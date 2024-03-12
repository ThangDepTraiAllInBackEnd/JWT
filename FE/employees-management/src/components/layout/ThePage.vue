<template>
  <router-view></router-view>
  <m-dialog
    v-if="isShowDialog"
    :msgs="msgs"
    :dialogType="dialogType"
    :togleDialog="togleDialog"
    :dialogIcon="dialogIcon"
  />
  <m-toast
    v-if="isShowToast"
    :type="toastType"
    :title="toastMsg"
    :togleToast="togleToast"
  />
  <m-loading v-if="isShowLoading" />
</template>

<script>
import MDialog from "../base/MDialog.vue";
import MToast from "../base/MToast.vue";
import MLoading from "../base/MLoading.vue";
import MResource from "../js/StringResource";
export default {
  name: "TheLayout",
  components: { MDialog, MToast, MLoading },
  created() {
    this.emitter.on(MResource.Event.TogleToast, (type, msg, status) => {
      this.togleToast(type, msg, status);
    });

    this.emitter.on(MResource.Event.TogleDialog, (msgs, type, icon) => {
      this.togleDialog(msgs, type, icon);
    });
    this.emitter.on(MResource.Event.TogleLoading, (status) => {
      this.togleLoading(status);
    });
  },
  data() {
    return {
      // for dialog
      isShowDialog: false,
      msgs: [],
      dialogType: "",
      dialogIcon: "",
      // for toast
      isShowToast: false,
      toastMsg: "",
      toastType: "",
      // for loading
      isShowLoading: false,
    };
  },
  methods: {
    /**
     * togle Dialog
     */
    togleDialog(msgs, type, icon) {
      this.msgs = msgs;
      this.dialogType = type;
      this.dialogIcon = icon;
      this.isShowDialog = !this.isShowDialog;
    },
    /**
     * togle toast message
     */
    togleToast(type, msg, status) {
      this.toastType = type;
      this.toastMsg = msg;
      this.isShowToast = status;
      // auto close after 3 seconds if toast is showing
      if (this.isShowToast === true) {
        setTimeout(() => {
          this.isShowToast = false;
        }, 3000);
      }
    },
    /**
     * togel Loading icon
     */
    togleLoading(status) {
      this.isShowLoading = status;
    },
  },
};
</script>

<style>
@font-face {
  font-family: hihi;
  src: url("../../components/assets/fonts/notosans-regular.2cb88a13.woff2");
}
body {
  font-family: hihi;
  margin: 0;
  font-size: 13px;
}
</style>
