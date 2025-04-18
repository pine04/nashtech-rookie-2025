import { useEffect, useState } from "react";

export default function useForm({ initialValues, validators, submit }) {
    const [formData, setFormData] = useState(initialValues);

    const initialErrors = {};
    Object.keys(initialValues).forEach(
        (fieldName) => (initialErrors[fieldName] = "")
    );
    const [errors, setErrors] = useState(initialErrors);

    const [isFormValid, setIsFormValid] = useState(false);

    useEffect(() => {
        let isValid = true;

        for (const [fieldName, validator] of Object.entries(validators)) {
            const error = validator(formData[fieldName], formData);
            if (error !== undefined) {
                isValid = false;
                break;
            }
        }

        setIsFormValid(isValid);
    }, [formData]);

    const handleChange = (e) => {
        const fieldName = e.target.name;
        const fieldType = e.target.type;

        let value;
        if (fieldType === "checkbox") {
            value = e.target.checked;
        } else {
            value = e.target.value;
        }

        setFormData((prev) => ({
            ...prev,
            [fieldName]: value,
        }));

        const validator = validators[fieldName];
        if (!validator) {
            return;
        }

        setErrors((prev) => ({
            ...prev,
            [fieldName]: validator(value, formData),
        }));
    };

    const handleBlur = (e) => {
        const fieldName = e.target.name;

        const validator = validators[fieldName];
        if (!validator) {
            return;
        }

        const fieldValue = formData[fieldName];

        setErrors((prev) => ({
            ...prev,
            [fieldName]: validator(fieldValue, formData),
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        submit(formData);
    };

    return {
        formData,
        errors,
        isFormValid,
        handleChange,
        handleBlur,
        handleSubmit,
    };
}
