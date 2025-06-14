import type { LoginPayload, LoginPayloadResponseDataModel } from "../types/auth";
import api from "./api";


async function login(data: LoginPayload) {
    let res = await api.post<LoginPayloadResponseDataModel>('/auth/login/password', data);
    return res.data;
}
export {login,type LoginPayload,type LoginPayloadResponseDataModel}