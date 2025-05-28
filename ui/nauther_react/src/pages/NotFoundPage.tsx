import React from 'react';
import { Button, Typography } from 'antd';
import { useNavigate } from 'react-router-dom';
import { colors } from '../theme/colors';
import notFound from '../assets/404.gif';
const { Title, Text } = Typography;

const NotFoundPage: React.FC = () => {
  const navigate = useNavigate();

  return (
    <div style={{
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
      justifyContent: 'center',
      minHeight: '100vh',
      background: colors.background.default,
      padding: '2rem',
      textAlign: 'center'
    }}>
      <div style={{
        maxWidth: '600px',
        width: '100%',
        marginBottom: '2rem'
      }}>
        <img 
          src={notFound} 
          alt="404 Not Found" 
          style={{
            width: '100%',
            maxWidth: '500px',
            height: 'auto',
            marginBottom: '2rem',
            borderRadius: '8px'
          }}
        />
        <Title level={2} style={{ 
          color: colors.text.primary,
          marginBottom: '1rem'
        }}>
          صفحه مورد نظر یافت نشد
        </Title>
        <Text style={{ 
          color: colors.text.secondary,
          fontSize: '1.1rem',
          marginBottom: '2rem',
          display: 'block'
        }}>
          متأسفانه صفحه‌ای که به دنبال آن هستید وجود ندارد یا به آدرس دیگری منتقل شده است.
        </Text>
        <Button 
          type="primary" 
          size="large"
          onClick={() => navigate('/')}
          style={{
            height: '40px',
            padding: '0 2rem'
          }}
        >
          بازگشت به صفحه اصلی
        </Button>
      </div>
    </div>
  );
};

export default NotFoundPage; 