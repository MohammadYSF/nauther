import React, { createContext, useContext, useEffect, useState, type ReactNode } from 'react';
import { login, type LoginPayload, type LoginPayloadResponseDataModel } from '../services/authService';
import { useNavigate } from 'react-router-dom';

interface AuthContextType {
    user: string | null;
    accessToken: string | null;
    loginUser: (payload: LoginPayload, returnUrl: string) => Promise<LoginPayloadResponseDataModel | undefined>;
    logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {

    const [user, setUser] = useState<string | null>(null);
    const [accessToken, setAccessToken] = useState<string | null>(localStorage.getItem("accesstoken"));
    const navigate = useNavigate();
    const loginUser = async (payload: LoginPayload, returnUrl: string) => {
        try {
            const res = await login(payload);
            if (res.data?.access_Token) {
                setUser(payload.username);
                setAccessToken(res.data.access_Token);
                localStorage.setItem("accesstoken", res.data.access_Token);
                navigate(returnUrl);
            }
            return res;
        } catch (error) {
            // Optionally handle error
            return undefined;
        }
    };

    const logout = () => {
        setUser(null);
        setAccessToken(null);
        localStorage.removeItem("accesstoken");
        // Optionally remove tokens from storage
    };

    return (
        <AuthContext.Provider value={{ user, accessToken, loginUser, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) throw new Error('useAuth must be used within an AuthProvider');
    return context;
};