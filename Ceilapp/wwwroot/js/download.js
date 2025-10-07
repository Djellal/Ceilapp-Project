// Function to download a file using JavaScript
window.downloadFile = (filename, contentType, content) => {
    // Convert base64 content to binary data
    const byteCharacters = atob(content);
    let byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    
    // Create a Blob with the binary data
    const blob = new Blob([byteArray], { type: contentType });
    
    // Create a temporary link element to trigger the download
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = filename;
    
    // Append the link to the document, click it, and remove it
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    
    // Clean up the object URL to free memory
    window.URL.revokeObjectURL(link.href);
};