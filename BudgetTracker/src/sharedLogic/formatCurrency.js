function formatCurrency(value) {
    let formattedAmount;

    if (value < 0) {
        const amountAsPositive = value * -1;
    
        formattedAmount = `- £${amountAsPositive.toFixed(2)}`;
    }
    else {
        formattedAmount = `£${value.toFixed(2)}`;
    }
    
    return formattedAmount;
}

export default formatCurrency;