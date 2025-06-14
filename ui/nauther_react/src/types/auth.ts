import type { BaseApiResponseModel } from "./baseApiResponseModel"

interface LoginPayload {
    username: string,
    password: string
};
interface LoginPayloadResponseDataModel extends BaseApiResponseModel<{access_Token:string}> {
};
export type {LoginPayload,LoginPayloadResponseDataModel};