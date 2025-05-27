import api from "./api";

interface LoginPayload {
    username: string,
    password: string
};
interface LoginPayloadResponseDataModel {
    statusCode: number,
    message: string,
    validationErrors: any,
    data: {
        access_Token: string
    }
}
async function login(data: LoginPayload) {
    let res = await api.post<LoginPayloadResponseDataModel>('/auth/login/password', data);
    return res.data;
}
export {login,type LoginPayload,type LoginPayloadResponseDataModel}