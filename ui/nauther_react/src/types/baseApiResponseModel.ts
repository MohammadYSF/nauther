interface BaseApiResponseModel<T> {
    statusCode: number,
    message: string,
    validationErrors: ValidationError[],
    data: T
};  
interface ValidationError{
    key:string,
    value:string
}
export type { BaseApiResponseModel }