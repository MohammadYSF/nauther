import React from 'react';
import { Form, Input, Button, Card, Typography, Row, Col } from 'antd';
import { login, type LoginPayload } from '../services/authService';
import { useAuth } from '../contexts/AuthContext';
import loginImg from '../assets/login_img.jpg';
import { colors } from '../theme/colors';

const { Title } = Typography;

const Login: React.FC = () => {
  const {loginUser} = useAuth();
  const onFinish = (values: { username: string; password: string }) => {
    loginUser(values as LoginPayload,"/");
  };

  return (
    <div style={{ 
      display: 'flex', 
      height: '100vh',
      overflow: 'hidden'
    }}>
      {/* Left side - Building Image */}
      <div style={{
        flex: 1,
        backgroundImage: `url(${loginImg})`,
        backgroundSize: 'cover',
        backgroundPosition: 'center',
        backgroundRepeat: 'no-repeat',
        position: 'relative',
        display: 'block'
      }}>
        <div style={{
          position: 'absolute',
          top: 0,
          left: 0,
          right: 0,
          bottom: 0,
          background: colors.overlay.dark,
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          padding: '2rem'
        }}>
          <div style={{ textAlign: 'center' }}>
            <Title level={2} style={{ 
              color: colors.primary.contrast, 
              marginBottom: '1rem' 
            }}>
              خوش آمدید
            </Title>
            <p style={{ 
              fontSize: '1.2rem', 
              opacity: 0.9,
              color: colors.primary.contrast 
            }}>
              به سیستم مدیریت دسترسی‌ها خوش آمدید
            </p>
          </div>
        </div>
      </div>

      {/* Right side - Login Form */}
      <div style={{
        flex: 1,
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        padding: '2rem'
      }}>
        <Card style={{ width: '100%', maxWidth: 400 }}>
          <Title level={3} style={{ textAlign: 'center', marginBottom: '2rem' }}>
            ورود به سیستم
          </Title>
          <Form
            name="login"
            layout="vertical"
            onFinish={onFinish}
            size="large"
          >
            <Form.Item
              label="نام کاربری"
              name="username"
              rules={[{ required: true, message: 'لطفا نام کاربری خود را وارد کنید!' }]}
            >
              <Input placeholder="نام کاربری" />
            </Form.Item>
            <Form.Item
              label="رمز عبور"
              name="password"
              rules={[{ required: true, message: 'لطفا رمز عبور خود را وارد کنید!' }]}
            >
              <Input.Password placeholder="رمز عبور" />
            </Form.Item>
            <Form.Item>
              <Button type="primary" htmlType="submit" block>
                ورود
              </Button>
            </Form.Item>
          </Form>
        </Card>
      </div>
    </div>
  );
};

export default Login;