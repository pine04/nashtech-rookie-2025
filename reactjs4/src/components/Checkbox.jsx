import clsx from "clsx"

export default function Checkbox({ name, value, label, required, error, onChange }) {
    return (
        <div>
            <label className="flex gap-2">
                <input
                    type="checkbox"
                    name={name}
                    checked={value}
                    onChange={onChange}
                />

                <p
                    className={clsx(
                        required &&
                        "after:content-['*'] after:text-red-500 after:font-bold after:ml-1"
                    )}
                >
                    {label}
                </p>
            </label>
            <p className="text-red-500">{error}</p>
        </div>
    )
}