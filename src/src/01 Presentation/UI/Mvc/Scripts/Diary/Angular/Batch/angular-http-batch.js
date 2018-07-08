/*
 * angular-http-batcher - v1.11.3 - 2015-08-27
 * https://github.com/jonsamwell/angular-http-batcher
 * Copyright (c) 2015 Jon Samwell;*/
! function(a, b) {
    "use strict";

    function c() {
        var a = [],
            c = "httpBatchAdapter",
            d = {
                maxBatchedRequestPerCall: 10,
                minimumBatchSize: 2,
                batchRequestCollectionDelay: 100,
                ignoredVerbs: ["head"],
                sendCookies: !1,
                enabled: !0,
                adapter: c
            };
        this.setAllowedBatchEndpoint = function(e, f, g) {
            var h = b.copy(d);
            void 0 !== g && (b.forEach(g, function(a, b) {
                h[b] = a
            }), b.forEach(h.ignoredVerbs, function(a, b) {
                h.ignoredVerbs[b] = a.toLowerCase()
            })), h.serviceUrl = e, h.batchEndpointUrl = f, h.adapter = h.adapter || c, a.push(h)
        }, this.getBatchConfig = function(b) {
            var c, d;
            for (d = 0; d < a.length && (c = a[d], !(b.indexOf(c.serviceUrl) > -1)); d += 1) c = void 0;
            return c
        }, this.canBatchCall = function(a, b) {
            var c = this.getBatchConfig(a),
                d = c ? c.canBatchRequest : void 0,
                e = !1;
            return c && c.enabled === !0 && (e = d ? d(a, b) : c.batchEndpointUrl !== a && -1 === a.indexOf(c.batchEndpointUrl) && -1 === c.ignoredVerbs.indexOf(b.toLowerCase())), e
        }, this.calculateBoundary = function() {
            return (new Date).getTime().toString()
        }, this.$get = [
            function() {
                return this
            }
        ]
    }

    function d(a, b, c, d, e) {
        this.request = a, this.statusCode = b, this.statusText = c, this.data = d, this.headers = e
    }

    function e(c, d, e) {
        function f(a, b) {
            var d, f, g, h, i = e.calculateBoundary(),
                k = {
                    method: "POST",
                    url: b.batchEndpointUrl,
                    cache: !1,
                    headers: b.batchRequestHeaders || {}
                }, l = [];
            for (k.headers[o.contentType] = "multipart/mixed; boundary=" + i, f = 0; f < a.length; f += 1) {
                if (g = a[f], d = j(g.url), l.push(o.doubleDash + i), b.batchPartRequestHeaders)
                    for (h in b.batchPartRequestHeaders) l.push(h + o.colon + o.singleSpace + b.batchPartRequestHeaders[h]);
                l.push("Content-Type: application/http; msgtype=request", o.emptyString), l.push(g.method + " " + encodeURI(d.relativeUrl) + " " + o.httpVersion), l.push("Host: " + d.host);
                for (h in g.headers) l.push(h + o.colon + o.singleSpace + g.headers[h]);
                b.sendCookies === !0 && c[0].cookie && c[0].cookie.length > 0 && l.push("Cookie: " + c[0].cookie), l.push(o.emptyString), g.data && l.push(g.data), l.push(o.emptyString)
            }
            return l.push(o.doubleDash + i + o.doubleDash), k.data = l.join(o.newline), k
        }

        function g(a, b, c) {
            var d, e, f = [],
                g = k(b.headers()["content-type"]),
                h = b.data.split(o.doubleDash + g + o.newline),
                i = 0;
            for (d = 0; d < h.length; d += 1) e = h[d], e !== o.emptyString && (f.push(m(e, a[i], g)), i += 1);
            return f
        }

        function h(a, b) {
            return !0
        }

        function i(a) {
            return a = a.trim ? a.trim() : a.replace(/^\s+|\s+$/g, "")
        }

        function j(a) {
            var b, c, e, f, g;
            if (a.indexOf("./") > -1 || a.indexOf("../") > -1) {
                var h = document.createElement("a");
                h.href = a, a = h.href
            }
            return a.indexOf("://") > -1 ? (f = a.indexOf("://") + 3, g = a.slice(f).split(o.forwardSlash), b = a.substring(0, f), c = g[0], e = function() {
                return delete g[0], g.join(o.forwardSlash)
            }()) : (e = a, b = d.location.protocol, c = d.location.host), {
                protocol: b,
                host: c,
                relativeUrl: e
            }
        }

        function k(a) {
            var b = "boundary=",
                c = a.indexOf(b),
                d = a.substring(c + b.length);
            return d = d.replace(/"/g, o.emptyString)
        }

        function l(a, c) {
            var d = c;
            return a = a.toLowerCase(), a.indexOf("json") > -1 && (d = b.fromJson(c)), d
        }

        function m(b, c, d) {
            var e, f, g, h, j, k, m = b.split(o.newline),
                n = {
                    headers: {}
                }, p = !1;
            for (f = 0; f < m.length; f += 1)
                if (e = m[f], e !== o.emptyString) {
                    if (void 0 === n.contentType && -1 !== e.indexOf("-Type") && -1 === e.indexOf("; msgtype=response")) n.contentType = e.split(o.forwardSlash)[1];
                    else if (void 0 !== n.contentType && p === !1) k = e.split(o.colon), n.headers[k[0]] = i(k[1]);
                    else if (void 0 === n.statusCode && -1 !== e.indexOf(o.httpVersion)) j = e.split(o.singleSpace), n.statusCode = parseInt(j[1], 10), n.statusText = j.slice(2).join(o.singleSpace);
                    else if (void 0 === n.data && p) {
                        for (n.data = "", g = 1, h = new RegExp("--" + d + "--", "i"); h.test(e) === !1 && f + g <= m.length;) n.data += e, e = m[f + g], g += 1;
                        n.data = l(n.contentType, n.data);
                        break
                    }
                } else p = void 0 !== n.contentType;
            return n.headers[o.contentType] = n.contentType, new a.ahb.HttpBatchResponseData(c, n.statusCode, n.statusText, n.data, n.headers)
        }
        var n = this,
            o = {
                httpVersion: "HTTP/1.1",
                contentType: "Content-Type",
                newline: "\r\n",
                emptyString: "",
                singleSpace: " ",
                forwardSlash: "/",
                doubleDash: "--",
                colon: ":"
            };
        n.key = "httpBatchAdapter", n.buildRequest = f, n.parseResponse = g, n.canBatchRequest = h
    }

    function f() {
        function b(a, b) {
            var c, d, e, f, g = {
                    method: "GET",
                    url: b.batchEndpointUrl + "?",
                    cache: !1,
                    headers: b.batchRequestHeaders || {}
                };
            for (d = 0; d < a.length; d += 1) e = a[d], f = e.url.split("?"), c = f[0].replace(b.serviceUrl, ""), f.length > 1 && (c += "?" + encodeURIComponent(f[1])), d > 0 && (g.url += "&"), g.url += d.toString() + "=" + c;
            return g
        }

        function c(b, c) {
            var d, e, f, g = [],
                h = c.data;
            for (d = 0; d < b.length; d += 1) e = b[d], f = h[d.toString()], g.push(new a.ahb.HttpBatchResponseData(e, f.statusCode, "", f.body, f.headers));
            return g
        }

        function d(a) {
            return "GET" === a.method
        }
        var e = this;
        e.key = "nodeJsMultiFetchAdapter", e.buildRequest = b, e.parseResponse = c, e.canBatchRequest = d
    }

    function g(a) {
        var b, c = "";
        for (b in a) c += b + ": " + a[b] + "\n";
        return c
    }

    function h() {
        var a = this.config.adapter;
        if ("object" == typeof a) {
            if (void 0 === a.buildRequest || void 0 === a.parseResponse) throw new Error('A custom adapter must contain the methods "buildRequest" and "parseResponse" - please see the docs')
        } else if ("string" == typeof a && (a = this.adapters[a], void 0 === a)) throw new Error("Unknown type of http batch adapter: " + this.config.adapter);
        return a
    }

    function i(a) {
        return this.requests.push(a), this.requests.length >= this.config.maxBatchedRequestPerCall && this.flush(), !0
    }

    function j(a) {
        return "string" == typeof a ? a.replace(")]}',\n", "") : a
    }

    function k() {
        var a = this,
            c = a.getAdapter(),
            d = c.buildRequest(a.requests, a.config);
        a.sendCallback(), a.$injector.get("$http")(d).then(function(d) {
            var e;
            d.data = j(d.data), e = c.parseResponse(a.requests, d, a.config), b.forEach(e, function(a) {
                a.request.callback(a.statusCode, a.data, g(a.headers), a.statusText)
            })
        }, function(c) {
            b.forEach(a.requests, function(a) {
                a.callback(c.statusCode, c.data, c.headers, c.statusText)
            })
        })
    }

    function l() {
        this.$timeout.cancel(this.currentTimeoutToken), this.currentTimeoutToken = void 0, this.send()
    }

    function m(a, c, d, e, f) {
        var g = this;
        this.$injector = a, this.$timeout = c, this.adapters = d, this.config = e, this.sendCallback = f, this.requests = [], this.currentTimeoutToken = c(function() {
            g.currentTimeoutToken = void 0, g.requests.length < g.config.minimumBatchSize ? (g.sendCallback(), b.forEach(g.requests, function(a) {
                a.continueDownNormalPipeline()
            })) : g.send(a)
        }, e.batchRequestCollectionDelay, !1)
    }

    function n(a, c, d, e, f) {
        function g(a, b) {
            return d.canBatchCall(a, b)
        }

        function h(b) {
            var e = d.getBatchConfig(b.url),
                f = k[e.batchEndpointUrl];
            void 0 === f && (f = new m(a, c, l, e, function() {
                delete k[e.batchEndpointUrl]
            }), k[e.batchEndpointUrl] = f), f.addRequest(b)
        }

        function i(a) {
            b.forEach(k, function(b, c) {
                var d = void 0 === a || a && c.toLocaleLowerCase() === a.toLocaleLowerCase();
                d && b.flush()
            })
        }
        var j = this,
            k = {}, l = {
                httpBatchAdapter: e,
                nodeJsMultiFetchAdapter: f
            };
        j.canBatchRequest = g, j.batchRequest = h, j.flush = i
    }

    function o(a, c) {
        var d = function(b, d, e, f, g, h, i, j) {
            var k = this,
                l = arguments;
            return c.canBatchRequest(d, b) ? void c.batchRequest({
                method: b,
                url: d,
                data: e,
                callback: f,
                headers: g,
                timeout: h,
                withCredentials: i,
                responseType: j,
                continueDownNormalPipeline: function() {
                    a.apply(k, l)
                }
            }) : a.apply(this, arguments)
        };
        return b.mock && b.forEach(a, function(a, b) {
            d[b] = a
        }), d
    }

    function p(a) {
        a.decorator("$httpBackend", o)
    }
    a.ahb = {
        name: "jcs.angular-http-batch"
    }, b.module(a.ahb.name, []), b.module(a.ahb.name).provider("httpBatchConfig", c), a.ahb.HttpBatchResponseData = d, e.$inject = ["$document", "$window", "httpBatchConfig"], b.module(a.ahb.name).service("httpBatchAdapter", e), b.module(a.ahb.name).service("nodeJsMultiFetchAdapter", f), m.prototype.getAdapter = h, m.prototype.send = k, m.prototype.addRequest = i, m.prototype.flush = l, n.$inject = ["$injector", "$timeout", "httpBatchConfig", "httpBatchAdapter", "nodeJsMultiFetchAdapter"], b.module(a.ahb.name).service("httpBatcher", n), o.$inject = ["$delegate", "httpBatcher"], p.$inject = ["$provide"], b.module(a.ahb.name).config(p)
}(window, angular);
