export const setLocalStorageWithExpiry = (key: string, value: any, ttl: number) => {
  const now = new Date();
  const item = {
    value: value,
    expiry: now.getTime() + (ttl * 60 * 1000)
  }
  localStorage.setItem(key, JSON.stringify(item));
}

export const getLocalStorageWithExpiry = (key: string) => {
  const itemStr = localStorage.getItem(key);
  if (!itemStr) {
    return null;
  }
  const item = JSON.parse(itemStr);
  const now = new Date();
  if (now.getTime() > item.expiry) {
    localStorage.removeItem(key);
    return null;
  }
  return item.value;
}

export const removeLocalStorage = (key: string) => {
  localStorage.removeItem(key);
}