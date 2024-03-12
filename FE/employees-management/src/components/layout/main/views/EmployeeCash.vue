<template>
  <div v-if="isAuthorize">
    <h1>Tiền mặt</h1>
    <h1>đây là admin</h1>
    <li v-for="d in departmentList" :key="d.DepartmentId">{{ d }}</li>
  </div>
  <div v-else>
    <h1>Bạn không có quyền xem nội dung này</h1>
  </div>
</template>

<script>
import MResource from "../../../js/StringResource";
import { apiHandle } from "../../../js/ApiHandle";
import { checkAuthentication } from "../../../js/TokenHandle";

export default {
  name: "EmployeeCash",
  async created() {
    await checkAuthentication(this.emitter);
    this.getDepartmentList();
  },
  data() {
    return {
      departmentList: [],
      isAuthorize: false,
    };
  },
  methods: {
    /**
     * get department list
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    async getDepartmentList() {
      const departmentResponse = await apiHandle(
        MResource.apiMethod.get,
        this.resource.apiData.admin,
        null,
        null,
        this.emitter
      );
      if (departmentResponse) {
        this.isAuthorize = true;
        this.departmentList = departmentResponse.data.Data;
      }
    },
  },
};
</script>

<style>
</style>