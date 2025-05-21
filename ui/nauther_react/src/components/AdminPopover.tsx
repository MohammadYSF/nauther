import { Popover, Typography, Avatar, Button } from 'antd';
import { CloseOutlined } from '@ant-design/icons';

interface RolePopoverProps {
  anchorEl: HTMLElement | null;
  onClose: () => void;
  open: boolean;
}

const admins = [
    'علی محمدی',
    'مهدی حسینی',
    'سهند افشردی'    

];

export default function AdminPopover({ anchorEl, onClose, open }: RolePopoverProps) {
  return (
    <Popover
      open={open}
      content={
        <div style={{ minWidth: 320, maxWidth: 350, direction: 'rtl', padding: 16, borderRadius: 16 }}>
          <div style={{ display: 'flex', alignItems: 'center', marginBottom: 8 }}>
            <Typography.Text style={{ fontWeight: 500, flex: 1 }}>سهیل کیانی</Typography.Text>
            <Avatar style={{ width: 24, height: 24, marginLeft: 8 }} />
            <Button type="text" size="small" icon={<CloseOutlined />} onClick={onClose} style={{ marginLeft: 'auto' }} />
          </div>
          <ul style={{ paddingRight: 18, margin: 0, direction: 'rtl' }}>
            {admins.map((admin, idx) => (
              <li key={idx} style={{ fontSize: 15, marginBottom: 8, wordBreak: 'break-word' }}>
                {admin}
              </li>
            ))}
          </ul>
        </div>
      }
      title={null}
      trigger="click"
      getPopupContainer={() => anchorEl || document.body}
      onOpenChange={visible => { if (!visible) onClose(); }}
      overlayStyle={{ padding: 0 }}
    />
  );
}