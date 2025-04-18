import clsx from "clsx";

export default function Select({ name, value, required, label, onChange, onBlur, error, children }) {
    return (
        <div>
            <label className="block">
                <p
                    className={clsx(
                        required &&
                        "after:content-['*'] after:text-red-500 after:font-bold after:ml-1"
                    )}
                >
                    {label}
                </p>

                <select
                    name={name}
                    value={value}
                    className="block w-full border border-gray-300 rounded-lg px-3 py-2"
                    onChange={onChange}
                    onBlur={onBlur}
                >
                    {children}
                </select>
            </label>
            <p className="text-red-500">{error}</p>
        </div>
    );
}