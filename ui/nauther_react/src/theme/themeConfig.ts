import type { ThemeConfig } from 'antd/es/config-provider/context';
import { colors } from './colors';

export const themeConfig: ThemeConfig = {
  token: {
    // Colors
    colorPrimary: colors.primary.main,
    colorPrimaryHover: colors.primary.dark,
    colorPrimaryActive: colors.primary.dark,
    colorBgContainer: colors.background.paper,
    colorBgLayout: colors.background.default,
    colorText: colors.text.primary,
    colorTextSecondary: colors.text.secondary,
    colorTextDisabled: colors.text.disabled,
    colorBorder: colors.border.main,
    colorBorderSecondary: colors.border.light,
    
    // Status colors
    colorSuccess: colors.status.success,
    colorWarning: colors.status.warning,
    colorError: colors.status.error,
    colorInfo: colors.status.info,

    // Border radius
    borderRadius: 8,
    
    // Font
    fontFamily: 'IRANSans, -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif',
    
    // Control height
    controlHeight: 40,
    
    // Padding
    padding: 16,
    paddingLG: 24,
    paddingSM: 12,
    
    // Margin
    margin: 16,
    marginLG: 24,
    marginSM: 12,
    
    // Animation
    motion: true,
    motionDurationMid: '0.2s',
    motionDurationSlow: '0.3s',
    motionEaseInOut: 'cubic-bezier(0.645, 0.045, 0.355, 1)',
    
    // Shadow
    boxShadow: '0 4px 12px rgba(0, 0, 0, 0.1)',
    boxShadowSecondary: '0 2px 8px rgba(0, 0, 0, 0.08)',
  },
  
  components: {
    Button: {
      colorPrimary: colors.primary.main,
      colorPrimaryHover: colors.primary.dark,
      colorPrimaryActive: colors.primary.dark,
      borderRadius: 6,
      controlHeight: 40,
      fontSize: 16,
    },
    Input: {
      colorBorder: colors.border.main,
      colorBorderHover: colors.primary.main,
      borderRadius: 6,
      controlHeight: 40,
    },
    Card: {
      colorBgContainer: colors.background.paper,
      borderRadius: 8,
      boxShadow: '0 4px 12px rgba(0, 0, 0, 0.1)',
    },
    Form: {
      labelColor: colors.text.primary,
      labelHeight: 32,
    },
    Typography: {
      colorText: colors.text.primary,
      colorTextHeading: colors.text.primary,
    },
    Select: {
      colorBorder: colors.border.main,
      colorBorderHover: colors.primary.main,
      borderRadius: 6,
      controlHeight: 40,
    },
  },
}; 