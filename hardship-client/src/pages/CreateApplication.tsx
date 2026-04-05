import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { createApplication } from "../api/hardshipApi";
import type { CreateHardshipApplicationRequest } from "../types/hardship";
import { Button, Form, Input, InputNumber, DatePicker, Alert, Typography } from "antd";
import dayjs from "dayjs";

const { Title } = Typography;

export default function CreateApplication(){
    const navigate = useNavigate();
    const [error, setError] = useState<string | null>(null);
    const [form] = Form.useForm();

    const handleSubmit = async (values: CreateHardshipApplicationRequest & { dateOfBirth: dayjs.Dayjs }) => {
        try {
            await createApplication({
                ...values,
                dateOfBirth: values.dateOfBirth.format('YYYY-MM-DD'),
            });
            navigate('/');
        } catch {
            setError('Failed to submit application. Please try again.');
        }
    };

    return (
        <div style={{ padding: 32, maxWidth: 600, margin: '0 auto' }}>
            <Title level={2}>New Hardship Application</Title>
            {error && <Alert description={error} type="error" style={{ marginBottom: 16 }} />}

            <Form form={form} layout="vertical" onFinish={handleSubmit}>
                <Form.Item name="firstName" label="First Name" rules={[{ required: true }]}>
                    <Input />
                </Form.Item>
                <Form.Item name="lastName" label="Last Name" rules={[{ required: true }]}>
                    <Input />
                </Form.Item>
                <Form.Item name="dateOfBirth" label="Date of Birth" rules={[{ required: true }]}>
                    <DatePicker style={{ width: '100%' }} />
                </Form.Item>
                <Form.Item name="email" label="Email" rules={[{ required: true }, { type: 'email' }]}>
                    <Input />
                </Form.Item>
                <Form.Item name="phone" label="Phone">
                    <Input />
                </Form.Item>
                <Form.Item name="income" label="Income" rules={[{ required: true }]}>
                    <InputNumber style={{ width: '100%' }} min={0} />
                </Form.Item>
                <Form.Item name="expenses" label="Expenses" rules={[{ required: true }]}>
                    <InputNumber style={{ width: '100%' }} min={0} />
                </Form.Item>
                <Form.Item name="hardshipReason" label="Hardship Reason">
                    <Input.TextArea rows={3} />
                </Form.Item>
                <Form.Item>
                    <Button type="primary" htmlType="submit" style={{ marginRight: 8 }}>Submit</Button>
                    <Button onClick={() => navigate('/')}>Cancel</Button>
                </Form.Item>
            </Form>
        </div>
    );

}