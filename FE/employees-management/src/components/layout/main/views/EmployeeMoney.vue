<template>
  <h1>Tiền gửi</h1>
</template>

<script>
import { checkAuthentication } from "../../../js/TokenHandle";
export default {
  name: "EmployeeMoney",
  async created() {
    await checkAuthentication(this.emitter);
  },
};
</script>

<style>
</style>