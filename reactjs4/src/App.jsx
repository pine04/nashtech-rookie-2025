import useForm from "./hooks/useForm";

import TextInput from "./components/TextInput";
import Select from "./components/Select";
import Option from "./components/Option";
import Checkbox from "./components/Checkbox";
import Button from "./components/Button";

const usernameRegex = /^[A-Za-z0-9]+$/;
const emailRegex =
    /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;

function App() {
    const { formData, errors, isFormValid, handleChange, handleBlur, handleSubmit } = useForm(
        {
            initialValues: {
                username: "",
                email: "",
                gender: "",
                password: "",
                retypePassword: "",
                agree: false,
            },
            validators: {
                username: (value) => {
                    if (!value) return "Username is required.";
                    if (value.length < 4)
                        return "Username must be at least 4 characters.";
                    if (!usernameRegex.test(value))
                        return "Username must only consist of characters: A-Z, a-z, 0-9.";
                },
                email: (value) => {
                    if (!value) return "Email is required.";
                    if (!emailRegex.test(value)) return "Email is invalid.";
                },
                gender: (value) => {
                    if (value !== "male" && value !== "female")
                        return "You must select a gender.";
                },
                password: (value) => {
                    if (!value) return "Password is required.";
                    if (value.length < 8)
                        return "Password must be at least 8 characters.";
                },
                retypePassword: (value, formData) => {
                    if (!value) return "You must retype your password.";
                    if (value !== formData.password)
                        return "Passwords do not match.";
                },
                agree: (value) => {
                    if (!value)
                        return "You must agree to the Terms of Service.";
                },
            },
            submit: (formData) => {
                alert(JSON.stringify(formData));
            }
        }
    );

    return (
        <form className="max-w-md mx-auto my-5 border border-gray-300 rounded-md p-8">
            <h1 className="font-bold text-2xl text-center mb-6">Registration form</h1>

            <div className="flex flex-col gap-4">
                <TextInput
                    type="text"
                    name="username"
                    value={formData.username}
                    label="Username"
                    required={true}
                    error={errors.username}
                    onChange={handleChange}
                    onBlur={handleBlur}
                />
                <TextInput
                    type="email"
                    name="email"
                    value={formData.email}
                    label="Email"
                    required={true}
                    error={errors.email}
                    onChange={handleChange}
                    onBlur={handleBlur}
                />
                <Select
                    name="gender"
                    value={formData.gender}
                    label="Gender"
                    required={false}
                    error={errors.gender}
                    onChange={handleChange}
                    onBlur={handleBlur}
                >
                    <Option value="" label="--SELECT--" />
                    <Option value="male" label="Male" />
                    <Option value="female" label="Female" />
                </Select>
                <TextInput
                    type="password"
                    name="password"
                    value={formData.password}
                    label="Password"
                    required={true}
                    error={errors.password}
                    onChange={handleChange}
                    onBlur={handleBlur}
                />
                <TextInput
                    type="password"
                    name="retypePassword"
                    value={formData.retypePassword}
                    label="Retype password"
                    required={true}
                    error={errors.retypePassword}
                    onChange={handleChange}
                    onBlur={handleBlur}
                />
                <Checkbox
                    name="agree"
                    value={formData.agree}
                    label="I agree with the Terms of Service."
                    required={true}
                    error={errors.agree}
                    onChange={handleChange}
                />
            </div>

            <div className="mt-6 flex items-center justify-center">
                <Button
                    type="submit"
                    label="Submit"
                    disabled={!isFormValid}
                    onClick={handleSubmit}
                />
            </div>
        </form>
    );
}

export default App;
