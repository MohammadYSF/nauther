import axios from 'axios';

export interface User {
    id: string;
    userCode: string;
    profileImage: string;
    username: string;
    phoneNumber: string;
    isActive: boolean;
}

interface GetAllUsersResponseDataModel_Raw {
    statusCode: number;
    message: string;
    validationErrors: any;
    data: {
        key: string;
        value: string; // JSON string
    }[];
    metadata: { total: number }
}
export interface GetAllUsersResponseDataModel {
    statusCode: number;
    message: string;
    validationErrors: any;
    data: User[];
    metadata: { total: number }
}

export async function getAllUsers(pageNumber: number, pageSize: number, search: string): Promise<GetAllUsersResponseDataModel> {
    const response = await axios.get<GetAllUsersResponseDataModel_Raw>('https://localhost:5001/api/User/all', {
        params: { pageNumber, pageSize, search },
        headers: { accept: '*/*' }
    });
    let x = response.data.data.map(item => ({
        ...JSON.parse(item.value),
        key: item.key,
    } as User));
    return {...response.data, data: x};
}
